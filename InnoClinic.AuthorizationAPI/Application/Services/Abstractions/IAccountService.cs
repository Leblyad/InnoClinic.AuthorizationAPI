using InnoClinic.AuthorizationAPI.Application.Services.AuthorizationDTO;
using InnoClinic.AuthorizationAPI.Core.Entities.AuthorizationDTO;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;

namespace InnoClinic.AuthorizationAPI.Application.Services.Abstractions
{
    public interface IAccountService
    {
        public Task<AuthenticatedUserInfo> AuthenticateUserAsync(UserForAuthenticationDto user);
        public Task<CreatedUserDto> CreateUserAsync(UserForCreationDto userForCreation);
        public Task ChangeUserRoleAsync(UserForChangingRole userForChangingRole);
        public Task SignOutUserAsync();
        public Task<CreatedUserDto> CreateUserTransactionAsync(UserForCreationDto userForCreation);
    }
}
