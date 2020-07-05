using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Services
{
    public class SqlHealthCheckService : IHealthCheck
    {
        private readonly IUnitOfWork _work;

        public SqlHealthCheckService(IUnitOfWork work)
        {
            _work = work;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _work.User.GetAsync(Guid.NewGuid());
                return HealthCheckResult.Healthy();
            }
            catch(Exception e)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
