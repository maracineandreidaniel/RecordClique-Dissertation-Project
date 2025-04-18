using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AlbumController : Controller
    {

        private readonly IAlbumService _albumService;
        private readonly IAssistantService _assistantService;

        public AlbumController(IAlbumService albumService, IAssistantService assistantService)
        {
            _albumService = albumService;
            _assistantService = assistantService;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> RemoveAlbum([FromRoute] Guid id)
        {
            var result = await _albumService.RemoveAlbum(id);
            return Ok(result);
        }

        [HttpGet("/albums")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetAlbums(int pageNumber, int pageSize, string? filterName, Guid? artistId, Guid? genreId, int? year, Guid? userId)

        {
            var albums = await _albumService.GetAlbums(pageNumber, pageSize, filterName, artistId, genreId, year, userId);
            return Ok(albums);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetAlbumById([FromRoute] Guid id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album != null)
            {
                return Ok(album);
            }
            return NotFound();
        }

        [HttpGet("userAllAlbums")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetUserAllAlbums(int pageNumber, int pageSize, Guid userId, int? type)
        {
            var albums = await _albumService.GetUserAllAlbums(pageNumber, pageSize,userId, type);
            if (albums != null)
            {
                return Ok(albums);
            }
            return NotFound();
        }

        [HttpPut("album-link")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> UpdateUserAlbumLink(Guid albumId, Guid userId, Boolean ind, int type)
        {
            var link = await _albumService.UpdateUserAlbumLink(albumId, userId, ind, type);
            if (link != null)
            {
                return Ok(link);
            }
            return NotFound();
        }

        [HttpGet("gpt-response")]
        [Authorize(Policy = "AdminUserPolicy")]
        public IActionResult GetAICustom(string text)
        {
            var response = _assistantService.GetAssistantResponse(text);
            return Ok(response);
        }

    }
}
