using FluentValidation;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProcessExplorerWeb.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        private readonly AuthenticationOptions _options;

        public LoginQueryValidator(IOptions<AuthenticationOptions> options)
        {
            _options = options.Value;

            RuleFor(p => p.Identifier).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.Password).MinimumLength(_options.IdentityPasswordOptions.RequiredLength);
        }
    }
}
