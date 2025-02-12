using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var statistics = await _statisticService.GetStatisticsAsync();
            return Ok(statistics);
        }

        [HttpGet("generate-report")]
        //[Authorize(Policy = "AdminUserPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        public async Task<IActionResult> GenerateReport()
        {
            var pdf = await _statisticService.GenerateAlbumReport();
            return pdf;
        }
    }
}
