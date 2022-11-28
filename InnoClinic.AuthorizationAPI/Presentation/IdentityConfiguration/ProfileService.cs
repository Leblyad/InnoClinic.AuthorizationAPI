using IdentityServer4.Models;
using IdentityServer4.Services;
using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InnoClinic.AuthorizationAPI.Presentation.Configuration
{
    public class ProfileService : IProfileService
    {
        protected UserManager<User> _userManager;

        public ProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim("roles", userRoles.FirstOrDefault())
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}
