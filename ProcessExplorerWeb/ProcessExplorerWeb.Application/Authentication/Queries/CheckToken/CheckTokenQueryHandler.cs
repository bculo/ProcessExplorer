using MediatR;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Authentication.Queries.CheckToken
{
    public class CheckTokenQueryHandler : IRequestHandler<CheckTokenQuery>
    {
        private readonly IAuthenticationService _service;

        public CheckTokenQueryHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(CheckTokenQuery request, CancellationToken cancellationToken)
        {
            var valid = await _service.IsTokenValid(request.Token);

            if (!valid)
                throw new ProcessExplorerException("Token not valid");

            return Unit.Value;
        }
    }
}
