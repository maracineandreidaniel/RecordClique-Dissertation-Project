using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_BusinessLogic.Strategies.Context;
using RecordClique_BusinessLogic.Strategies.StatisticsStrategy.Abstractions;
using RecordClique_BusinessLogic.Strategies.StatisticsStrategy;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly StatisticContext _statisticContext = new StatisticContext();

        public StatisticService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
            //_statisticContext = statisticContext;
        }

        public async Task<List<StatisticDTO>> GetStatisticsAsync()
        {
            var strategies = new List<IStatisticStrategy>
            {
                new BestRatedAlbumsStrategy(_albumRepository),
                new WorstRatedAlbumsStrategy(_albumRepository),
                new TopFavouriteAlbumsStrategy(_albumRepository),
                new TopListeningAlbumsStrategy(_albumRepository),
                new TopWishlistAlbumsStrategy(_albumRepository)
            };

            var allStatistics = new List<StatisticDTO>();

            foreach (var strategy in strategies)
            {
                _statisticContext.SetStrategy(strategy);
                var result = await _statisticContext.ExecuteStrategyAsync();
                allStatistics.AddRange(result);
            }

            return allStatistics;
        }
    }
}
