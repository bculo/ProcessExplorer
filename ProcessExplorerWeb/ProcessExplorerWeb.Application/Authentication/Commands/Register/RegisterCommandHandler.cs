using MediatR;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IAuthenticationService _service;

        public RegisterCommandHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var (result, user) = await _service.RegisterUser(request.UserName, request.Email, request.Password, null);

            if (!result.Succeeded)
                throw new ProcessExplorerException(result.Errors);

            return Unit.Value;
        }
    }
}
