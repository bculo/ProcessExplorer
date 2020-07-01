using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<LoginQueryResponseDto>
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}
