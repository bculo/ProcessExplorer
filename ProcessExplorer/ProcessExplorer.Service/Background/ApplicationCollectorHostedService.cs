using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Background
{
    /// <summary>
    /// Background service for collectiong applications
    /// </summary>
    public class ApplicationCollectorHostedService : BackgroundService
    {
        private readonly ILoggerWrapper _logger;
        private readonly IServiceProvider _provider;

        public ApplicationCollectorHostedService(IServiceProvider provider, ILoggerWrapper logger)
        {
            _provider = provider;
            _logger = logger;
        }

        /// <summary>
        /// Collect application
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInfo($"{nameof(ApplicationCollectorHostedService)} service running.");

                using (var scope = _provider.CreateScope())
                {
                    var behaviour = scope.ServiceProvider.GetRequiredService<IApplicationCollectorBehaviour>();
                    await behaviour.Collect();
                }

                await Task.Delay(30000);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInfo($"{nameof(ApplicationCollectorHostedService)} service is stopping.");

            await Task.CompletedTask;
        }
    }
}
