using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;

namespace InnoClinic.AuthorizationAPI.Core.Entities.Contracts
{
    public interface IAuthenticationManager
    {
        Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuthentication);
        Task<(string accessToken, string refreshToken)> GetTokens(UserForAuthenticationDto userForAuthentication, string userName);
        Task SignOut();
    }
}
