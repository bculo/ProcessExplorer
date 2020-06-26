using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ProcessExplorerWeb.Infrastructure;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            IHost host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            try
            {
                await DLLInfrastructureConfiguration.ConfigureStorage(scope.ServiceProvider);
                await host.RunAsync();
            }
            catch (Exception error)
            {
                logger.Error(error);
                throw error;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
