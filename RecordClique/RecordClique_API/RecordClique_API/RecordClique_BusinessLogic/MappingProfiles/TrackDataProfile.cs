using AutoMapper;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Entities;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class TrackDataProfile : Profile
    {
        public TrackDataProfile()
        {
            CreateMap<Track, TrackDTO>().ReverseMap();
        }
    }
}
