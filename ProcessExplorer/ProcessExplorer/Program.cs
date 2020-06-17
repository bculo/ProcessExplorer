using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Configurations;
using ProcessExplorer.Configurations;
using ProcessExplorer.Persistence;
using ProcessExplorer.Persistence.Configurations;
using ProcessExplorer.Service.Configurations;
using ProcessExplorer.Service.Log;
using System;
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

        /// <summary>
        /// Startup configration
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task MainAsync(string[] args)
        {
            IConfiguration configuration = GetConfiguration();
            IServiceCollection services = ConfigureStartUpDependencies(configuration);

            ConfigureLogger(services, configuration);

            #region CALL STARTUP POINT 

            IServiceProvider provider = services.BuildServiceProvider();

            ApplyDatabaseMigrations(provider);

            await provider.GetRequiredService<Startup>().StartApplication(provider);

            #endregion
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
        /// Configure logger
        /// </summary>
        private static void ConfigureLogger(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(configuration);
            });

            services.AddTransient<ILoggerWrapper, LoggerWrapper>();
        }

        /// <summary>
        /// Configure IServiceCollection and IConfiguration
        /// </summary>
        private static IServiceCollection ConfigureStartUpDependencies(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();

            //Add startup point
            services.AddTransient<Startup>();

            //Configure current project
            services.ApplyConfigurationConsole(configuration);

            //Configure application layer
            services.ApplyConfigurationApplication(configuration);

            //Configure persistence layer
            services.ApplyConfigurationPersistence(configuration);

            //Configure service layer
            services.ApplyConfigurationService(configuration);

            return services;
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
