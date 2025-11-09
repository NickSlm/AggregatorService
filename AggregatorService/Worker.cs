using AggregatorService.Interfaces;

namespace AggregatorService
{
    public class Worker : BackgroundService
    {
        private readonly ILoggingService _loggingService;
        private readonly IBlizzardApiService _blizzardApiService;

        public Worker(ILoggingService loggingService, IBlizzardApiService blizzardApiService)
        {
            _loggingService = loggingService;
            _blizzardApiService = blizzardApiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);
                await _blizzardApiService.GetRawData();
            }
        }
    }
}
