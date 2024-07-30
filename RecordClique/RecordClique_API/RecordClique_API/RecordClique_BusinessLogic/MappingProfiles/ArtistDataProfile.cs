using AutoMapper;
using RecordClique.Models;
using RecordClique.Models.DTOs;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class ArtistDataProfile : Profile
    {
        public ArtistDataProfile()
        {
            CreateMap<Artist, ArtistDto>().ReverseMap();
        }
    }
}
