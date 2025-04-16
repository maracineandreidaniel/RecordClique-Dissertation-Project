using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.Services;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class GenreController : Controller
    {
        public readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            this._genreService = genreService;
        }

        [HttpGet("SelectOptions")]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetRecordLabelSelectOptions()
        {
            var result = await _genreService.GetGenreSelectOptions();
            return Ok(result);
        }
    }
}
