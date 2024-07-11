using Microsoft.AspNetCore.Mvc;
using RecordClique.Models.DTOs;
using RecordClique_API.Services.Interfaces;
using RecordClique_BusinessLogic.Services;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllArtists()
        {
            var artists = _artistService.GetAllArtists();
            return Ok(artists);
        }

        [HttpPost("add")]
        public IActionResult AddArtist([FromBody]ArtistDto artistRequest)
        {
            _artistService.AddArtist(artistRequest);
            return Ok("Artist was added!");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> RemoveArtist([FromRoute] Guid id)
        {
            var result = await _artistService.DeleteArtist(id);
            return Ok(result);
        }

    }
}
