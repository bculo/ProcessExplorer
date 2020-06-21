using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Behaviours
{
    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        protected readonly ILogger<TRequest> _logger;

        public ExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception e)
            {
                string requestName = typeof(TRequest).Name;
                _logger.LogError(e, "Behaviour ExceptionBehaviour: {Name}, {@Request}, {@Error}", requestName, request, e);
                throw;
            }
        }
    }
}
