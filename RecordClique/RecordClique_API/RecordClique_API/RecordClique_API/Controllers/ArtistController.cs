using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddArtist([FromBody]ArtistDto artistRequest)
        {
            var artist = await _artistService.AddArtist(artistRequest);
            return Ok(artist);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> RemoveArtist([FromRoute] Guid id)
        {
            var result = await _artistService.DeleteArtist(id);
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetArtistById([FromRoute] Guid id)
        {
            var artist = await _artistService.GetArtistById(id);
            if (artist != null)
            {
                return Ok(artist);
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateArtist([FromBody]ArtistDto updateArtistRequest)
        {
            try
            {
                var artist = await _artistService.UpdateArtist(updateArtistRequest);
                if (artist == null)
                {
                    return NotFound();
                }
                return Ok(artist);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpGet("/artists")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetArtists(int pageNumber, int pageSize, string? filterName)

        {
            var artists = await _artistService.GetArtists(pageNumber, pageSize, filterName);
            return Ok(artists);
        }

        [HttpGet("SelectOptions")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetArtistSelectOptions()
        {
            var result = await _artistService.GetArtistSelectOptions();
            return Ok(result);
        }

    }
}
