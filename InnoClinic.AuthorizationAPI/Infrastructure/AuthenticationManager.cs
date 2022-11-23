﻿using InnoClinic.AuthorizationAPI.Core.Entities.Contracts;
using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.AuthorizationAPI.Infrastructure
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuth)
        {
            var user = await _userManager.FindByEmailAsync(userForAuth.Email);

            var res = await _signInManager.PasswordSignInAsync(user.UserName, userForAuth.Password, false, false);

            if (res.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<(string accessToken, string refreshToken)> GetTokens(UserForAuthenticationDto user, string userName)
        {
            var client = _httpClientFactory.CreateClient();
            var tokenRoute = _configuration.GetValue<string>("Routes:TokenRoute");

            PasswordTokenRequest tokenRequest = new PasswordTokenRequest()
            {
                Address = tokenRoute,
                ClientId = "APIClient",
                Scope = "offline_access openid",
                UserName = userName,
                Password = user.Password

            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenRequest);

            return (tokenResponse.AccessToken, tokenResponse.RefreshToken);
        }
    }
}
