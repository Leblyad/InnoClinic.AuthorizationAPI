using AuthorizationAPI.Core.Entities.Contracts;
using AuthorizationAPI.Core.Entities.Models;
using AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationAPI.Infrastructure
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuth)
        {
            var user = await _userManager.FindByNameAsync(userForAuth.UserName);

            var res = await _signInManager.PasswordSignInAsync(userForAuth.UserName, userForAuth.Password, false, false);

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

        public async Task<(string accessToken, string refreshToken)> GetTokens(UserForAuthenticationDto user)
        {
            var client = _httpClientFactory.CreateClient();
            PasswordTokenRequest tokenRequest = new PasswordTokenRequest()
            {
                Address = "https://localhost:7141/connect/token",
                ClientId = "APIClient",
                Scope = "offline_access",
                UserName = user.UserName,
                Password = user.Password,
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenRequest);

            return (tokenResponse.AccessToken, tokenResponse.RefreshToken);
        }
    }
}
