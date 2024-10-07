using AutoMapper;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class UserAlbumLinkDataProfile : Profile
    {
        public UserAlbumLinkDataProfile() {
            CreateMap<UserAlbumLink, UserAlbumLinkDTO>().ReverseMap();
        }
    }
}
