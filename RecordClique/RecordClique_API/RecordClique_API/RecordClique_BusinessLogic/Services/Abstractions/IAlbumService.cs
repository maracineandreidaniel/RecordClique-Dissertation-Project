using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Helpers;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IAlbumService
    {
        Task<AlbumDto> AddAlbum(AlbumDto album);
        Task<AlbumDto> UpdateAlbum(AlbumDto albumRequest);
        Task<string> DeleteAlbum(Guid id);
        Task<PaginatedResult<AlbumDto>> GetAlbums(int pageNumber, int pageSize, string? filterName);
        Task<AlbumDto> GetAlbumById(Guid id);
    }
}
