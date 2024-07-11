using Microsoft.AspNetCore.Mvc;
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
    }
}
