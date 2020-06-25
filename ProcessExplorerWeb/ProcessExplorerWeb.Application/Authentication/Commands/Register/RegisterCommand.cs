using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Authentication.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Email { get; set; }
    }
}
