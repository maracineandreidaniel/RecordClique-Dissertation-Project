using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Helpers;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IAlbumService
    {
        Task<AlbumDto> AddAlbum(AlbumDto album);
        Task<AlbumDto> UpdateAlbum(AlbumDto albumRequest);
        Task<object> RemoveAlbum(Guid id);
        Task<PaginatedResult<AlbumDto>> GetAlbums(int pageNumber, int pageSize, string? filterName, Guid? artistId, Guid? genreId, int? year);
        Task<AlbumDto> GetAlbumById(Guid id);
    }
}
