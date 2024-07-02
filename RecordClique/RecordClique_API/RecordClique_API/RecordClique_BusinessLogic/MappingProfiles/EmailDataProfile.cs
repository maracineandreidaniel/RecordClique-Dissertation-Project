using AutoMapper;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Entities;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class EmailDataProfile : Profile
    {
        public EmailDataProfile()
        {
            CreateMap<Email, EmailDto>().ReverseMap();
        }
    }
}
