using InnoClinic.AuthorizationAPI.Application.Services.Abstractions;
using InnoClinic.AuthorizationAPI.Core.Entities.Contracts;
using InnoClinic.AuthorizationAPI.Core.Entities.Enums;
using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using InnoClinic.AuthorizationAPI.Core.Exceptions;
using InnoClinic.AuthorizationAPI.Core.Exceptions.UserClassExceptions;
using InnoClinic.AuthorizationAPI.Core.Entities.AuthorizationDTO;

namespace InnoClinic.AuthorizationAPI.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authManager = authManager;
            _mapper = mapper;
        }

        public async Task ChangeUserRoleAsync(string email, string role)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                throw new UserNotFoundException(email);

            if (!await _roleManager.RoleExistsAsync(role))
                throw new RoleNotFoundException(role);

            var previousRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            await _userManager.RemoveFromRoleAsync(user, previousRole);
            await _userManager.AddToRoleAsync(user, role);
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

            var result = await _userManager.CreateAsync(user, userForCreation.Password);

            var userRole = (UserRole)Enum.Parse(typeof(UserRole), userForCreation.Role);

            await _userManager.AddToRoleAsync(user, userRole.ToString());

            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Code}: {error.Description}\n";
                }
                throw new Exception(errors);
            }

            var userDto = _mapper.Map<CreatedUserDto>(user);
            userDto.Role = userRole.ToString();

            return userDto;
        }
    }
}
