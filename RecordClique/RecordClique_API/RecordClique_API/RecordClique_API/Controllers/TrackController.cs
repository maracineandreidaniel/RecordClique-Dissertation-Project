using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class TrackController : Controller
    {
       private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService)
        {
            this._trackService = trackService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetTracks(Guid albumId)
        {
            var tracks = await _trackService.GetTracks(albumId);
            return Ok(tracks);
        }
    }
}
