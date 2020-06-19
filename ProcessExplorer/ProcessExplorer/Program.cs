using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ProcessExplorer.Application.Configurations;
using ProcessExplorer.Configurations;
using ProcessExplorer.Interfaces;
using ProcessExplorer.Persistence.Configurations;
using ProcessExplorer.Service.Background;
using ProcessExplorer.Service.Configurations;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting application...");
            MainAsync(args).Wait();
            Console.WriteLine("Closing application...");
        }

        /// <summary>
        /// Startup configration
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task MainAsync(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            IHost host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            try
            {
                await scope.ServiceProvider.GetRequiredService<IStartupPoint>().Start();
                await host.RunAsync();
            }
            catch (Exception error)
            {
                logger.Error(error);
                Console.WriteLine(error.Message);
            }
        }

        /// <summary>
        /// Create IHostBuilder instance
        /// </summary>
        /// <param name="args">input arguments for console application</param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = GetConfiguration();
                    ConfigureStartUpDependencies(services, configuration);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseConsoleLifetime()
                .UseNLog();

            return hostBuilder;
        }

        /// <summary>
        /// Configure IServiceCollection and IConfiguration
        /// </summary>
        private static void ConfigureStartUpDependencies(IServiceCollection services, IConfiguration configuration)
        {
            //Configure current project
            services.ApplyConfigurationConsoleApplication(configuration);

            //Configure application layer
            services.ApplyConfigurationApplication(configuration);

            //Configure persistence layer
            services.ApplyConfigurationPersistence(configuration);

            //Configure service layer
            services.ApplyConfigurationService(configuration);
        }

        /// <summary>
        /// Get configuraton file (appsettings.json)
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            return configuration;
        }
    }
}
