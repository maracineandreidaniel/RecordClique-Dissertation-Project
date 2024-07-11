using AutoMapper;
using RecordClique.Models;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
