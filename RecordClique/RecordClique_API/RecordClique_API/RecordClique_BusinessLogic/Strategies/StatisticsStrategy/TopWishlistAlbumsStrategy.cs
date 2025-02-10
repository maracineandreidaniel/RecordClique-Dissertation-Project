using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Strategies.StatisticsStrategy.Abstractions;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Strategies.StatisticsStrategy
{
    public class TopWishlistAlbumsStrategy : IStatisticStrategy
    {
        private readonly IRepository<Album> _albumRepository;

        public TopWishlistAlbumsStrategy(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }
        
        public async Task<List<StatisticDTO>> GetStatisticsAsync()
        {
            var albums = await _albumRepository.GetAll();

            var statistics = await albums.Include(a => a.Reviews)
                .Include(a => a.AlbumArtistLinks)
                .ThenInclude(a => a.Artist)
                .Include(a => a.UserAlbumLinks)
                .Select(a => new StatisticDTO
                {
                    AlbumTitle = a.Title,
                    ArtistName = string.Join(",", a.AlbumArtistLinks.Select(aa => aa.Artist.Name)),
                    Result = a.UserAlbumLinks.Select(l => l.IsOnWishlist).Count(),
                    Type = Constants.Statistic.TopWishlistAlbumsStrategy
                })
                .Take(3)
                .OrderByDescending(a => a.Result)
                .ThenBy(a => a.AlbumTitle)
                .ToListAsync();

            return statistics;
        }
    }
}
