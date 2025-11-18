using AggregatorService.Interfaces;

namespace AggregatorService
{
    public class Worker : BackgroundService
    {
        private readonly ILoggingService _loggingService;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILoggingService loggingService, IServiceScopeFactory scopeFactory)
        {
            _loggingService = loggingService;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbService = scope.ServiceProvider.GetService<IDbService>();
            await dbService.SaveSnapshot();

            _loggingService.LogInfo("Snapshot saved");
        }
    }
}
