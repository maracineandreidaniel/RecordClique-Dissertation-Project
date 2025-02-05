using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this._reviewService = reviewService;
        }

        [HttpGet("/reviews")]
        //[Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetReviews(int pageNumber, int pageSize, Guid? albumId)

        {
            var reviews = await _reviewService.GetReviews(pageNumber, pageSize, albumId);
            return Ok(reviews);
        }
    }
}
