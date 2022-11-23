using InnoClinic.AuthorizationAPI.Core.Entities.AuthorizationDTO;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;

namespace InnoClinic.AuthorizationAPI.Application.Services.Abstractions
{
    public interface IAccountService
    {
        public Task<AuthenticatedUserInfo> AuthenticateUser(UserForAuthenticationDto user);
        public Task<CreatedUserDto> CreateUser(UserForCreationDto userForCreation, string role);
        public Task AddRoleToUser(string login, string role);
        public Task SignOutUser();
    }
}
