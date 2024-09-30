using RecordClique.Models.DTOs;
using RecordClique_DataAccess.Helpers;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IArtistService
    {
        Task<ArtistDto> GetArtistById(Guid id);
        Task<ArtistDto> AddArtist(ArtistDto artist);
        Task<ArtistDto> UpdateArtist(ArtistDto artistRequest);
        Task<object> DeleteArtist(Guid id);
        Task<PaginatedResult<ArtistDto>> GetArtists(int pageNumber, int pageSize, string? filterName);
        Task<List<SelectOptionResult>> GetArtistSelectOptions();
    }
}
