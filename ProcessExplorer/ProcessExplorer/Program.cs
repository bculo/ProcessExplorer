using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Configurations;
using ProcessExplorer.Configurations;
using ProcessExplorer.Persistence;
using ProcessExplorer.Persistence.Configurations;
using ProcessExplorer.Service.Configurations;
using ProcessExplorer.Service.Log;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProcessExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Starting application...");
            MainAsync(args).Wait();
            System.Console.WriteLine("Closing application...");

            Console.ReadLine();
        }

        /*
         *    var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", true);
        if (args != null) config.AddCommandLine(args);
    })
    .ConfigureServices((hostingContext, services) =>
    {
        services.AddHostedService<MyHostedService>();
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration);
        logging.AddConsole();
    });

await builder.RunConsoleAsync();
         */

        /// <summary>
        /// Startup configration
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task MainAsync(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
                  .ConfigureServices((hostContext, services) =>
                  {
                      IConfiguration configuration = GetConfiguration();
                      ConfigureStartUpDependencies(services, configuration);
                      ApplyDatabaseMigrations(services.BuildServiceProvider());
                  })
                  .ConfigureLogging(logging =>
                  {
                      logging.ClearProviders();
                      logging.SetMinimumLevel(LogLevel.Trace);
                  })
                  .UseNLog();

            await hostBuilder.RunConsoleAsync();
        }

        /// <summary>
        /// Apply database migrations or create database if doesnt exist
        /// </summary>
        /// <param name="provider"></param>
        private static void ApplyDatabaseMigrations(IServiceProvider provider)
        {
            ProcessDbContextHelper.ApplyMigrations(provider);
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
