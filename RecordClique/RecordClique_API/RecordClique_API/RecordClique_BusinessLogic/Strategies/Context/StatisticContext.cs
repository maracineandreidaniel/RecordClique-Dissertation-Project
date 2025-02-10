using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Strategies.StatisticsStrategy.Abstractions;

namespace RecordClique_BusinessLogic.Strategies.Context
{
    public class StatisticContext
    {
        private IStatisticStrategy _strategy;

        public StatisticContext()
        {
           
        }

        public void SetStrategy(IStatisticStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<List<StatisticDTO>> ExecuteStrategyAsync()
        {
            if(_strategy == null)
            {
                throw new InvalidOperationException("Strategy is not set");
            }

            return await _strategy.GetStatisticsAsync();
        }
    }
}
