using System;
using System.Linq;
using AutoMapper;
using RecordClique.Models;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Entities;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class AlbumDataProfile : Profile
    {
        public AlbumDataProfile()
        {
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => src.RecordLabel))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()))
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(a => new ArtistDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList()))
                .ReverseMap()
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.Artists, opt => opt.Ignore());

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<RecordLabel, RecordLabelDto>().ReverseMap();
        }
    }
}
