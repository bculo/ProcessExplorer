using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Behaviours
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly PerformanceOptions _options;

        public PerformanceBehavior(IOptions<PerformanceOptions> options, ILogger<TRequest> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var response = await next();
            stopWatch.Stop();

            if (stopWatch.ElapsedMilliseconds > _options.MaxMiliseconds)
            {
                string requestName = typeof(TRequest).Name;
                long miliSeconds = stopWatch.ElapsedMilliseconds;
                _logger.LogWarning("Behavoir PerformanceBehavior: {Name}, Miliseconds {$MiliSeconds}", requestName, miliSeconds);
            }

            return response;
        }
    }
}
