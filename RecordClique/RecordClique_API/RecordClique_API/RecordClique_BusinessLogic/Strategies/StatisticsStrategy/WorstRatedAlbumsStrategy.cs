using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Strategies.StatisticsStrategy.Abstractions;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Strategies.StatisticsStrategy
{
    public class WorstRatedAlbumsStrategy : IStatisticStrategy
    {
        private readonly IRepository<Album> _albumRepository;

        public WorstRatedAlbumsStrategy(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<List<StatisticDTO>> GetStatisticsAsync()
        {
            var albums = await _albumRepository.GetAll();

            var statistics = await albums.Include(a => a.Reviews)
                .Include(a => a.AlbumArtistLinks)
                .ThenInclude(a => a.Artist)
                .Select(a => new StatisticDTO
                {
                    AlbumTitle = a.Title,
                    ArtistName = a.AlbumArtistLinks.Any() == true ? string.Join(",", a.AlbumArtistLinks.Select(aa => aa.Artist.Name)) : "",
                    Result = a.Reviews.Select(r => r.Rating).Count() > 0 ? a.Reviews.Select(r => r.Rating).Sum() / a.Reviews.Select(r => r.Rating).Count() : 0,
                    Type = Constants.Statistic.Type.WorstRatedAlbumsStrategyType
                })
                .Take(3)
                .OrderBy(a => a.Result)
                .ThenBy(a => a.AlbumTitle)
                .ToListAsync();

            return statistics;
        }
    }
}
