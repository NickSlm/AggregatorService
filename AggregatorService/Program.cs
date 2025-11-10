using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.WindowsServices;
using AggregatorService.Interfaces;
using AggregatorService.Services;
using Polly;
using System.Diagnostics.Metrics;
using System;

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

            host.Run();
        }

        public static void ConfigureMyServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<Worker>();
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