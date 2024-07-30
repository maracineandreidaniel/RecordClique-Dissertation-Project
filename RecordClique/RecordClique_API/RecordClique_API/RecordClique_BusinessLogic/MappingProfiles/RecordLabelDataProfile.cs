using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecordClique.Models.DTOs;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class RecordLabelDataProfile : Profile
    {
        public RecordLabelDataProfile()
        {
            CreateMap<RecordLabel,RecordLabelDto>().ReverseMap();
        }
    }
}
