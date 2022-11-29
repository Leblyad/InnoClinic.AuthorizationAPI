using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;

namespace InnoClinic.AuthorizationAPI.Application.Services.AuthorizationDTO
{
    public class UserForCreationByAdminDto : UserForCreationDto
    {
        public string Role { get; set; }
    }
}
