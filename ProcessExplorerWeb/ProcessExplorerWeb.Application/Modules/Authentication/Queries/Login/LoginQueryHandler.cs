using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Events;
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
        private readonly IMediator _mediator;

        public LoginQueryHandler(IAuthenticationService service,
            IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public async Task<LoginQueryResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
           //Check user credentials
            var result = await _service.UserCredentialsValid(request.Identifier, request.Password);

            if (!result.Succeeded)
                throw new  ProcessExplorerException("Invalid credentials");

            //generate JWT toekn
            var tokenInfo = await _service.GenerateJwtToken(request.Identifier);

            //notify other users that new user loged in
            if(request.IsWebApp)
                await _mediator.Publish(new UserLogedInEvent(tokenInfo.User.Id));

            //map to dto instance
            var dto = tokenInfo.Adapt<LoginQueryResponseDto>();

            return dto;
        }
    }
}
