using AggregatorService.Interfaces;

namespace AggregatorService
{
    public class Worker : BackgroundService
    {
        private readonly ILoggingService _loggingService;
        private readonly IBlizzardAuthService _blizzardAuthService;

        public Worker(ILoggingService loggingService, IBlizzardAuthService blizzardAuthService)
        {
            _loggingService = loggingService;
            _blizzardAuthService = blizzardAuthService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(20000);
                var token = await _blizzardAuthService.GetAccessTokenAsync();
                Console.WriteLine(token);
            
            }
        }
    }
}
