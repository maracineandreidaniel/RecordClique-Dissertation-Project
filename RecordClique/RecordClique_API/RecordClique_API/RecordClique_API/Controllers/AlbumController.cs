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
        public async Task<IActionResult> UpdateAlbum([FromBody] AlbumDto albumRequest)
        {
            try
            {
                var album = await _albumService.UpdateAlbum(albumRequest);
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
            var result = await _albumService.RemoveAlbum(id);
            return Ok(result);
        }

        [HttpGet("/albums")]
        //[Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetAlbums(int pageNumber, int pageSize, string? filterName)

        {
            var albums = await _albumService.GetAlbums(pageNumber, pageSize, filterName);
            return Ok(albums);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAlbumById([FromRoute] Guid id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album != null)
            {
                return Ok(album);
            }
            return NotFound();
        }

  
    }
}
