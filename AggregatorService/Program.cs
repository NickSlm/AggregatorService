using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.WindowsServices;
using AggregatorService.Interfaces;
using AggregatorService.Services;
using Polly;
using System.Diagnostics.Metrics;
using System;
using AggregatorService.Data;
using Microsoft.EntityFrameworkCore;

namespace AggregatorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "MyService";
                })
                .ConfigureServices(ConfigureMyServices)
                .ConfigureLogging(ConfigureMyLogging)
                .Build();


            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MyDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                    throw;
                }
            }

            host.Run();
        }

        public static void ConfigureMyServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<Worker>();
            services.AddScoped<IDbService ,DbService>();

            services.AddDbContextFactory<MyDbContext>(options =>   
            {
                options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddSingleton<IBlizzardAuthService, BlizzardAuthService>();
            services.AddSingleton<IBlizzardApiService, BlizzardApiService>();
            services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>( sp =>
            {
                var logger = sp.GetRequiredService<ILoggingService>();

                return Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(
                    3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),(result, timespan, retryNo, context) =>
                {logger.LogWarning($"Retry No{retryNo} due to {result.Exception?.Message ?? result.Result.StatusCode.ToString()}");}
                );
            });

        }
        public static void ConfigureMyLogging(HostBuilderContext context, ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddDebug();
            logging.AddEventLog();
        }

    }
}