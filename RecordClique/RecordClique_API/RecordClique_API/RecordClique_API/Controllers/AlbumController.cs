using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AlbumController : Controller
    {

        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAlbums()
        {
            var albums = await _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpPost]
        public async Task<IActionResult> AddAlbum([FromBody] AlbumDto albumRequest)
        {
            try
            {
                var album = await _albumService.AddAlbum(albumRequest);
                return Ok(album);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAlbum(Guid albumId, [FromBody] AlbumDto albumRequest)
        {
            try
            {
                var album = await _albumService.UpdateAlbum(albumId, albumRequest);
                return Ok(album);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> RemoveAlbum([FromRoute] Guid id)
        {
            var result = await _albumService.DeleteAlbum(id);
            return Ok(result);
        }
    }
}
