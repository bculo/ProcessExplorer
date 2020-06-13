using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Configurations;
using ProcessExplorer.Configurations;
using ProcessExplorer.Persistence.Configurations;
using ProcessExplorer.Service.Configurations;
using ProcessExplorer.Service.Services.System;

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
            ConfigureLogger();

            IConfiguration configuration = GetConfiguration();
            IServiceCollection collection = ConfigureStartUpDependencies(configuration);

            #region CALL STARTUP POINT 

            IServiceProvider provider = collection.BuildServiceProvider();
            await provider.GetRequiredService<Startup>().StartApplication(provider);

            #endregion
        }

        /// <summary>
        /// Configure logger
        /// </summary>
        private static void ConfigureLogger()
        {
            
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
