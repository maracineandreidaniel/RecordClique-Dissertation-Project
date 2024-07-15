using System;
using AutoMapper;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Entities;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class AlbumDataProfile : Profile
    {
        public AlbumDataProfile()
        {
            CreateMap<Album, AlbumDto>()
                            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.AlbumGenreLinks.Select(x => x.Genre)))
                            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.AlbumArtistLinks.Select(x => x.Artist)))
                            .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => src.RecordLabel))
                            .ReverseMap()
                            .ForMember(dest => dest.AlbumGenreLinks, opt => opt.MapFrom(src => src.Genres.Select(x => new AlbumGenreLink { FK_AlbumId = src.Id, FK_GenreId = x.Id })))
                            .ForMember(dest => dest.AlbumArtistLinks, opt => opt.MapFrom(src => src.Artists.Select(x => new AlbumArtistLink { FK_AlbumId = src.Id, FK_ArtistId = x.Id })))
                            .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => src.RecordLabel));
        }
    }
}
