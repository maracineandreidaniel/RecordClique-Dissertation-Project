using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Helpers;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IReviewService
    {
        Task<PaginatedResult<ReviewDTO>> GetReviews(int pageNumber, int pageSize, Guid? albumId);
    }
}
