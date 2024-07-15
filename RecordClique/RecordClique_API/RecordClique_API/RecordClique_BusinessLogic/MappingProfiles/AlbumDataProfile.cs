using AutoMapper;
using RecordClique.Models;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Entities;
using System.Linq;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class AlbumDataProfile : Profile
    {
        public AlbumDataProfile()
        {
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.AlbumGenreLinks.Select(x => new GenreDto
                {
                    Id = x.Genre.Id,
                    Name = x.Genre.Name
                })))
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.AlbumArtistLinks.Select(x => new ArtistDto
                {
                    Id = x.Artist.Id,
                    Name = x.Artist.Name,
                })))
                .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => new RecordLabelDto
                {
                    Id = src.RecordLabel.Id,
                    Name = src.RecordLabel.Name
                }))
                .ReverseMap()
                .ForMember(dest => dest.AlbumGenreLinks, opt => opt.MapFrom(src => src.Genres.Select(x => new AlbumGenreLink
                {
                    FK_AlbumId = src.Id,
                    FK_GenreId = x.Id
                })))
                .ForMember(dest => dest.AlbumArtistLinks, opt => opt.MapFrom(src => src.Artists.Select(x => new AlbumArtistLink
                {
                    FK_AlbumId = src.Id,
                    FK_ArtistId = x.Id
                })))
                .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => new RecordLabel
                {
                    Id = src.RecordLabel.Id,
                    Name = src.RecordLabel.Name
                }));
        }
    }
}
