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

            DateTime designatedDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 22, 00, 00, DateTimeKind.Utc);

            while (!stoppingToken.IsCancellationRequested)
            {

                TimeSpan delay = designatedDate - DateTime.UtcNow;
                if (delay < TimeSpan.Zero)
                {
                    delay = TimeSpan.Zero;
                }

                _loggingService.LogInfo($"Next Cleanup scheduled at {designatedDate} UTC");

                await Task.Delay(delay, stoppingToken);

                int attempt = 0;

                while(attempt < 3)
                {
                    attempt++;
                    try
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var dbService = scope.ServiceProvider.GetService<IDbService>();
                        await dbService.CleanOldRecords();

                        _loggingService.LogInfo("Records Removed");

                        break;
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        _loggingService.LogError(ex, $"Error {ex} while removing old records");
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                    }
                }
                designatedDate = designatedDate.AddDays(1);
            }
        }
    }
}
