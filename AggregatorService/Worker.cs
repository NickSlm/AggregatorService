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

            while (!stoppingToken.IsCancellationRequested) 
            {

                DateTime designatedTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 23, 0, 0, DateTimeKind.Utc);
                if (designatedTime <= DateTime.UtcNow)
                {
                    designatedTime.AddDays(1);
                }
                var delay = designatedTime - DateTime.UtcNow;

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


                        //TODO: change to once a day at 23:59
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
            }
        }
    }
}
