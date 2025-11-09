using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.WindowsServices;
using AggregatorService.Interfaces;
using AggregatorService.Services;

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