using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using AutoMapper;
using InnoClinic.AuthorizationAPI.Core.Entities.AuthorizationDTO;

namespace InnoClinic.AuthorizationAPI.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForCreationDto, User>();
            CreateMap<User, CreatedUserDto>();
        }
    }
}
