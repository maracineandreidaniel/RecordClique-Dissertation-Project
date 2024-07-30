using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAllAlbums();
        Task<AlbumDto> AddAlbum(AlbumDto album);
        Task<AlbumDto> UpdateAlbum(AlbumDto albumRequest);
        Task<string> DeleteAlbum(Guid id);
    }
}
