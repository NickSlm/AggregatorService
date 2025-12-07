using Shared.Interfaces;
using Shared.Services;
using Microsoft.EntityFrameworkCore;
using CleanupService.Data;

namespace CleanupService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName ="DbCleanup";
                })
                .ConfigureServices(ConfigureMyServices)
                .ConfigureLogging(ConfigureMyLogging)
                .Build();

            host.Run();
        }


        private static void ConfigureMyServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddDbContextFactory<MyDbContext>(options =>
            {
                options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        private static void ConfigureMyLogging(HostBuilderContext context, ILoggingBuilder logging) 
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddDebug();
            logging.AddEventLog(options =>
            {
                options.SourceName = "DbCleanup";
                options.LogName = "Application";
                options.Filter = (category, logLevel) => logLevel >= LogLevel.Information;
            });
        }

    }
}