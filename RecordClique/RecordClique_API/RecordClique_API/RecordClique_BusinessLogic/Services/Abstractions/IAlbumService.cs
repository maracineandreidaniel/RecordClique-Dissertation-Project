using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAllAlbums();
        Task<AlbumDto> AddAlbum(AlbumDto album);
        Task<AlbumDto> UpdateAlbum(Guid albumId, AlbumDto albumRequest);
        Task<string> DeleteAlbum(Guid id);
    }
}
