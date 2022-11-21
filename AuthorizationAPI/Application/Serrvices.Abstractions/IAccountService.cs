using AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;

namespace AuthorizationAPI.Application.Serrvices.Abstractions
{
    public interface IAccountService
    {
        public Task<AuthenticatedUserInfo> AuthenticateUser(UserForAuthenticationDto user);
        public Task CreateUser(UserForCreationDto userForCreation, string role);
        public Task AddRoleToUser(string login, string role);
        public Task SignOutUser();
    }
}
