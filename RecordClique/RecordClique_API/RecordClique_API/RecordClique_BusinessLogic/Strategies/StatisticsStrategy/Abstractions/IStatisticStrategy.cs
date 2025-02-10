using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Strategies.StatisticsStrategy.Abstractions
{
    public interface IStatisticStrategy
    {
        Task<List<StatisticDTO>> GetStatisticsAsync();
    }
}
