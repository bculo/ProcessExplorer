using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Services.System;

namespace ProcessExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        /// <summary>
        /// Startup configration
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task MainAsync(string[] args)
        {
            ConfigureLogger();

            IServiceCollection collection = ConfigureStartUpDependencies();
            IConfiguration configuration = GetConfiguration();

            #region CALL STARTUP POINT 

            IServiceProvider provider = collection.BuildServiceProvider();
            await provider.GetRequiredService<Startup>().StartApplication(configuration, collection);

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
        private static IServiceCollection ConfigureStartUpDependencies()
        {
            IServiceCollection services = new ServiceCollection();

            //Add startup point
            services.AddTransient<Startup>();

            //Platform information singleton
            services.AddSingleton(GetPlatormInformationService());

            return services;
        }

        /// <summary>
        /// Get operating system information (Linx or Windows)
        /// </summary>
        /// <returns></returns>
        private static IPlatformInformationService GetPlatormInformationService()
        {
            return PlatformInformationServiceFactory.CreatePlatformInformationService();
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
