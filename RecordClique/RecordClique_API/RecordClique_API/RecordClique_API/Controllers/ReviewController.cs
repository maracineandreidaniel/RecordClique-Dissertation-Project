using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.DTOs;
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

        [HttpGet]
        [Authorize(Policy = "AdminUserPolicy")]
        public async Task<IActionResult> GetReviews(int pageNumber, int pageSize, Guid? albumId)
        {
            var reviews = await _reviewService.GetReviews(pageNumber, pageSize, albumId);
            return Ok(reviews);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateReview(ReviewDTO reviewDto)
        {
            var review = await _reviewService.CreateReview(reviewDto);
            return Ok(review);
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            var review = await _reviewService.DeleteReview(reviewId);
            return Ok(review);
        }
    }
}
