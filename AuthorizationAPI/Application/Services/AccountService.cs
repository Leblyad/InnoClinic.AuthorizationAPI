using AuthorizationAPI.Application.Serrvices.Abstractions;
using AuthorizationAPI.Core.Entities.Contracts;
using AuthorizationAPI.Core.Entities.Models;
using AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationAPI.Application.Services
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

        public async Task AddRoleToUser(string login, string role)
        {
            var user = await _userManager.FindByNameAsync(login);

            if (user == null)
                throw new Exception("User not found");

            if (!await _roleManager.RoleExistsAsync(role))
                throw new Exception("Role not exists");

            //var identityRole = await _roleManager.FindByNameAsync(role);
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("roles", role));
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<AuthenticatedUserInfo> AuthenticateUser(UserForAuthenticationDto user)
        {
            var validUser = await _authManager.ReturnUserIfValid(user);

            if (validUser == null)
            {
                throw new Exception("Unauthorized");
            }

            var tokens = await _authManager.GetTokens(user);

            return new AuthenticatedUserInfo
            {
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken,
                UserRoles = await _userManager.GetRolesAsync(validUser)
            };
        }

        public async Task SignOutUser()
        {
            await _authManager.SignOut();
        }

        public async Task CreateUser(UserForCreationDto userForCreation)
        {
            var user = _mapper.Map<User>(userForCreation);

            var result = await _userManager.CreateAsync(user, userForCreation.Password);

            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Code}: {error.Description}\n";
                }
                throw new Exception(errors);
            }
        }
    }
}
