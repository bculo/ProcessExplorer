using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Behaviours;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Background
{
    public class CommunicationTypeHostedService : BackgroundService
    {
        private readonly ILoggerWrapper _logger;
        private readonly IServiceProvider _provider;

        public CommunicationTypeHostedService(IServiceProvider provider, ILoggerWrapper logger)
        {
            _provider = provider;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInfo($"{nameof(CommunicationTypeHostedService)} service running.");

                using (var scope = _provider.CreateScope())
                {
                    var behaviour = scope.ServiceProvider.GetRequiredService<ICommunicationTypeBehaviour>();
                    await behaviour.Check();
                }

                await Task.Delay(30000); //collect every thirty seconds
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInfo($"{nameof(CommunicationTypeHostedService)} service is stopping.");

            await Task.CompletedTask;
        }
    }
}
