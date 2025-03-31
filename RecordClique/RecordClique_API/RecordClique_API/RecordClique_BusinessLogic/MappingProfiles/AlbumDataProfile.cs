using AutoMapper;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.MappingProfiles
{
    public class AlbumDataProfile : Profile
    {
        public AlbumDataProfile()
        {
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.AlbumGenreLinks.Select(x => x.Genre.Id)))
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.AlbumArtistLinks.Select(x => x.Artist.Id)))
                .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => src.FK_RecordLabelId))
                .ForMember(dest => dest.ArtistsNames, opt => opt.MapFrom(src => string.Join(", ", src.AlbumArtistLinks.Select(x => x.Artist.Name))))
                .ForMember(dest => dest.GenresNames, opt => opt.MapFrom(src => string.Join(", ", src.AlbumGenreLinks.Select(x => x.Genre.Name))))
                .ForMember(dest => dest.RecordLabelName, opt => opt.MapFrom(src => src.RecordLabel.Name))
                .ReverseMap()
                .ForMember(dest => dest.FK_RecordLabelId, opt => opt.MapFrom(src => src.RecordLabel))
                .ForMember(dest => dest.RecordLabel, opt => opt.Ignore());
        }
    }
}
