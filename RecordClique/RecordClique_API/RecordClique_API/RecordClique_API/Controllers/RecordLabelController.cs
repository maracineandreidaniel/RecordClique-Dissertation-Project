using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class RecordLabelController : Controller
    {
        private readonly IRecordLabelService _recordLabelService;
        public RecordLabelController(IRecordLabelService recordLabelService)
        {
            this._recordLabelService = recordLabelService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllRecordLabels()
        {
            var recordLabels = await _recordLabelService.GetAllRecordLabels();
            return Ok(recordLabels);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecordLabel([FromBody] RecordLabelDto recordLabelRequest)
        {
            var recordLabel = await _recordLabelService.AddRecordLabel(recordLabelRequest);
            return Ok(recordLabel);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> RemoveRecordLabel([FromRoute] Guid id)
        {
            var result = await _recordLabelService.DeleteRecordLabel(id);
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetRecordLabelById([FromRoute] Guid id)
        {
            var recordLabel = await _recordLabelService.GetRecordLabelById(id);
            if (recordLabel != null)
            {
                return Ok(recordLabel);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecordLabel([FromBody] RecordLabelDto recordLabelRequest)
        {
            try
            {
                var recordLabel = await _recordLabelService.UpdateRecordLabel(recordLabelRequest);
                if (recordLabel == null)
                {
                    return NotFound();
                }
                return Ok(recordLabel);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }
    }
}
