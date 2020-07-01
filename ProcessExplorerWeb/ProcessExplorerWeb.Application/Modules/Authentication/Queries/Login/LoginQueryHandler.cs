using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResponseDto>
    {
        private readonly IAuthenticationService _service;

        public LoginQueryHandler(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task<LoginQueryResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
           
            var result = await _service.UserCredentialsValid(request.Identifier, request.Password);

            if (!result.Succeeded)
                throw new  ProcessExplorerException("Invalid credentials");

            var tokenInfo = await _service.GenerateJwtToken(request.Identifier);

            var dto = tokenInfo.Adapt<LoginQueryResponseDto>();

            return dto;
        }
    }
}
