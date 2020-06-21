using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Behaviours
{
    public class PreRequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        protected readonly ILogger<TRequest> _logger;
        protected readonly ICurrentUserService _userService;
        protected readonly IAuthenticationService _identity;

        public PreRequestLogger(ILogger<TRequest> logger,
            ICurrentUserService userService,
            IAuthenticationService identityService)
        {
            _logger = logger;
            _userService = userService;
            _identity = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            Guid userId = _userService.UserId;
            string userName = string.Empty;

            if (userId != Guid.Empty)
                userName = (await _identity.GetUserByIdentifier(userId.ToString()))?.UserName ?? string.Empty;

            _logger.LogInformation("Request: {Name}, {$UserId}, {$UserName}, {@Request}",
                requestName, userId, userName, request);
        }
    }
}
