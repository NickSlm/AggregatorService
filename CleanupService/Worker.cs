using CleanupService.Data;

namespace CleanupService
{
    public class Worker : BackgroundService
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //using var scope = _scopeFactory.CreateScope();
                //var dbService = scope.ServiceProvider.GetService<>(IDbService);
                //await dbService.CleanOldRecords();
            }
        }
    }
}
