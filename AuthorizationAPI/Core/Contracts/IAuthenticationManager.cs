using AuthorizationAPI.Core.Entities.DataTransferObjects;

namespace AuthorizationAPI.Core.Entities.Contracts
{
    public interface IAuthenticationManager
    {
        Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuthentication);
        Task<(string accessToken, string refreshToken)> GetTokens(UserForAuthenticationDto userForAuthentication);
    }
}
