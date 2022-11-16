using AuthorizationAPI.Core.Entities.Models;
using AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;

namespace AuthorizationAPI.Core.Entities.Contracts
{
    public interface IAuthenticationManager
    {
        Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuthentication);
        Task<(string accessToken, string refreshToken)> GetTokens(UserForAuthenticationDto userForAuthentication);
        Task SignOut();
    }
}
