using CleanupService.Data;
using CleanupService.Interfaces;
using Shared.Interfaces;

namespace CleanupService
{
    public class Worker : BackgroundService
    {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILoggingService _loggingService;

        public Worker(ILoggingService loggingService, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _loggingService = loggingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbService = scope.ServiceProvider.GetService<IDbService>();
                    await dbService.CleanOldRecords();

                    _loggingService.LogInfo("Records Removed");
                    await Task.Delay(10000, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _loggingService.LogError(ex, $"Error {ex} while removing old records");
                }
            {
            }
        }
    }
}
