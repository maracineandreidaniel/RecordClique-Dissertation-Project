using AutoMapper;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Entities;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class UserDataProfile : Profile
    {
        public UserDataProfile() {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
