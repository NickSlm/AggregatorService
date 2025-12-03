

namespace CleanupService
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName("DbCleanup");
                })
                .ConfigureServices(ConfigureMyServices)
                .ConfigureLogging()
                .Build();


        }


        private static void ConfigureMyServices(HostBuilderContext context, IServiceCollection services)
        {


        }

        private static void ConfigureMyLogging()
        {

        }

    }
}