using RecordClique.Models.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistDto>> GetAllArtists();

        Task<ArtistDto> GetArtistById(Guid id);

        Task<ArtistDto> AddArtist(ArtistDto artist);

        Task<ArtistDto> UpdateArtist(ArtistDto artistRequest);

        Task<string> DeleteArtist(Guid id);
    }
}
