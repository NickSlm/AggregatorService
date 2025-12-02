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

            DateTime designatedTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 7, 30, 00, DateTimeKind.Utc);
            while (!stoppingToken.IsCancellationRequested) 
            {

                var delay = designatedTime - DateTime.UtcNow;

                if (delay < TimeSpan.Zero)
                {
                    delay = TimeSpan.Zero;
                }
                _loggingService.LogInfo($"Next Snapshot scheduled at {designatedTime} UTC");

                await Task.Delay(delay, stoppingToken);

                int attempt = 0;

                while (attempt < 3)
                {
                    try
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var dbService = scope.ServiceProvider.GetService<IDbService>();
                        await dbService.SaveSnapshot();

                        _loggingService.LogInfo($"Snapshot saved on {DateTime.UtcNow}");

                        break;

                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        _loggingService.LogError(ex, $"Failed saving snapshot, attempt:{attempt}");
                        attempt++;
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                    }
                }

                designatedTime = designatedTime.AddDays(1);
            }
        }
    }
}
