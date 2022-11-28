using AutoMapper;
using InnoClinic.AuthorizationAPI.Application.Services.Abstractions;
using InnoClinic.AuthorizationAPI.Application.Services.AuthorizationDTO;
using InnoClinic.AuthorizationAPI.Core.Entities.AuthorizationDTO;
using InnoClinic.AuthorizationAPI.Core.Entities.Contracts;
using InnoClinic.AuthorizationAPI.Core.Entities.Enums;
using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using InnoClinic.AuthorizationAPI.Core.Exceptions;
using InnoClinic.AuthorizationAPI.Core.Exceptions.UserClassExceptions;
using InnoClinic.AuthorizationAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.AuthorizationAPI.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;
        private readonly AuthenticationDbContext _dbContext;

        public AccountService(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IAuthenticationManager authManager, AuthenticationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authManager = authManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task ChangeUserRoleAsync(UserForChangingRole userForChangingRole)
        {
            var user = await _userManager.FindByEmailAsync(userForChangingRole.Email);

            if (user == null)
                throw new UserNotFoundException(userForChangingRole.Role);

            if (!await _roleManager.RoleExistsAsync(userForChangingRole.Role))
                throw new RoleNotFoundException(userForChangingRole.Role);

            var previousRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            await _userManager.RemoveFromRoleAsync(user, previousRole);
            await _userManager.AddToRoleAsync(user, userForChangingRole.Role);
        }

        public async Task<AuthenticatedUserInfo> AuthenticateUserAsync(UserForAuthenticationDto user)
        {
            var validUser = await _authManager.ReturnUserIfValid(user);

            if (validUser == null)
            {
                throw new UnauthorizedException();
            }

            var tokens = await _authManager.GetTokens(user, validUser.UserName);
            var role = _userManager.GetRolesAsync(validUser).Result.FirstOrDefault().ToString();

            return new AuthenticatedUserInfo
            {
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken,
                UserRole = role
            };
        }

        public async Task SignOutUserAsync()
        {
            await _authManager.SignOut();
        }

        public async Task<CreatedUserDto> CreateUserAsync(UserForCreationDto userForCreation)
        {
            if (!Enum.IsDefined(typeof(UserRole), userForCreation.Role))
                throw new RoleNotFoundException(userForCreation.Role);

            var user = _mapper.Map<User>(userForCreation);

            var isUserCreated = await _userManager.CreateAsync(user, userForCreation.Password);

            if (!isUserCreated.Succeeded)
            {
                var errors = "";
                foreach (var error in isUserCreated.Errors)
                {
                    errors += $"{error.Code}: {error.Description}\n";
                }
                throw new Exception(errors);
            }

            var userRole = (UserRole)Enum.Parse(typeof(UserRole), userForCreation.Role);

            var isRoleAdded = await _userManager.AddToRoleAsync(user, userRole.ToString());

            if (!isRoleAdded.Succeeded)
            {
                var errors = "";
                foreach (var error in isRoleAdded.Errors)
                {
                    errors += $"{error.Code}: {error.Description}\n";
                }
                throw new Exception(errors);
            }

            var userDto = _mapper.Map<CreatedUserDto>(user);
            userDto.Role = userRole.ToString();

            return userDto;
        }

        public async Task<CreatedUserDto> CreateUserTransactionAsync(UserForCreationDto userForCreation)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            if (!Enum.IsDefined(typeof(UserRole), userForCreation.Role))
                throw new RoleNotFoundException(userForCreation.Role);

            var user = _mapper.Map<User>(userForCreation);

            var isUserCreated = await _userManager.CreateAsync(user, userForCreation.Password);

            if (!isUserCreated.Succeeded)
            {
                var errors = "";
                foreach (var error in isUserCreated.Errors)
                {
                    errors += $"{error.Code}: {error.Description}\n";
                }
                throw new Exception(errors);
            }

            var userRole = (UserRole)Enum.Parse(typeof(UserRole), userForCreation.Role);

            var isRoleAdded = await _userManager.AddToRoleAsync(user, userRole.ToString());

            if (!isRoleAdded.Succeeded)
            {
                var errors = "";
                foreach (var error in isRoleAdded.Errors)
                {
                    errors += $"{error.Code}: {error.Description}\n";
                }
                throw new Exception(errors);
            }

            transaction.Commit();

            var userDto = _mapper.Map<CreatedUserDto>(user);
            userDto.Role = userRole.ToString();

            return userDto;
        }
    }
}
