using AutoMapper;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class ReviewDataProfile : Profile
    {
        public ReviewDataProfile()
        {
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}
