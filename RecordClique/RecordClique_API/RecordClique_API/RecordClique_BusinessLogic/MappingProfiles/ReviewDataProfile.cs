using AutoMapper;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class ReviewDataProfile : Profile
    {
        public ReviewDataProfile()
        {
            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
