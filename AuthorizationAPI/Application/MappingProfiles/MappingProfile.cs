using AuthorizationAPI.Core.Entities.Models;
using AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using AutoMapper;

namespace AuthorizationAPI.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForCreationDto, User>();
        }
    }
}
