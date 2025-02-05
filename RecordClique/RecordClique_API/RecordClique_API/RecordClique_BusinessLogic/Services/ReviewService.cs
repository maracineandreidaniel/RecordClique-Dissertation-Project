using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_DataAccess.Helpers;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IRepository<Review> reviewRepository, IMapper mapper)
        {
            this._reviewRepository = reviewRepository;
            this._mapper = mapper;
        }

        public async Task<PaginatedResult<ReviewDTO>> GetReviews(int pageNumber, int pageSize, Guid? albumId)
        {
            var query = await _reviewRepository.GetAll();
             query = query.Where(r => r.FK_AlbumId == albumId);

            var totalItems = await query.CountAsync();

            var reviews = await query
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();

            var reviewDtos = reviews.Select(s =>
                _mapper.Map<ReviewDTO>(s)
            ).ToList();

            return new PaginatedResult<ReviewDTO>
            {
                Items = reviewDtos,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
