using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Log;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Background
{
    /// <summary>
    /// Background service for collectiong processes
    /// </summary>
    public class ProcessCollectorHostedService : BackgroundService
    {
        private readonly ILoggerWrapper _logger;
        private readonly IServiceProvider _provider;

        public ProcessCollectorHostedService(IServiceProvider provider, ILoggerWrapper logger)
        {
            _provider = provider;
            _logger = logger;
        }

        /// <summary>
        /// Collector processes
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInfo($"{nameof(ProcessCollectorHostedService)} service running.");

                using (var scope = _provider.CreateScope())
                {
                    var behaviour = scope.ServiceProvider.GetRequiredService<IProcessBehaviour>();
                    await behaviour.Collect();
                }

                await Task.Delay(30000);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInfo($"{nameof(ProcessCollectorHostedService)} service is stopping.");

            await Task.CompletedTask;
        }
    }
}
