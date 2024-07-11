using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAllAlbums();
    }
}
