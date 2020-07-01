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
    public class SyncHostedService : BackgroundService
    {
        private readonly ILoggerWrapper _logger;
        private readonly IServiceProvider _provider;

        public SyncHostedService(IServiceProvider provider, ILoggerWrapper logger)
        {
            _provider = provider;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInfo($"{nameof(SyncHostedService)} service running.");

                using (var scope = _provider.CreateScope())
                {
                    var behaviour = scope.ServiceProvider.GetRequiredService<ISyncBehaviour>();
                    await behaviour.Synchronize();
                }

                await Task.Delay(300000); // sync session every 5 minute
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInfo($"{nameof(SyncHostedService)} service is stopping.");

            await Task.CompletedTask;
        }
    }
}
