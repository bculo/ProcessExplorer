using FluentValidation;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IAuthenticationService _auth;
        private readonly AuthenticationOptions _options;

        public RegisterCommandValidator(IAuthenticationService auth, 
            IOptions<AuthenticationOptions> options)
        {
            _auth = auth;
            _options = options.Value;

            RuleFor(p => p.Email).NotEmpty();
            When(x => x.Email != null, () => {
                RuleFor(p => p.Email).EmailAddress().DependentRules(() =>
                    {
                        RuleFor(p => p.Email).MustAsync(UniqueEmail).WithMessage("The specified email already taken.");
                    });
            });

            RuleFor(p => p.UserName).NotEmpty();
            When(x => x.Email != null, () => {
                RuleFor(p => p.UserName).MinimumLength(_options.IdentityUserOptions.MinimumUsernameLength);
                RuleFor(p => p.UserName).MustAsync(UniqueUserName).WithMessage("The specified username already taken.");
            });

            RuleFor(p => p.Password).NotEmpty().Must((model, pw) => MustBeSame(model.RepeatPassword, pw)).WithMessage("Password does not match");
            RuleFor(p => p.RepeatPassword).NotEmpty().Must((model, pw) => MustBeSame(model.RepeatPassword, pw)).WithMessage("RepeatPassword does not match");
        }

        public async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _auth.GetUserByIdentifier(email);
            return (user == null) ? true : false;
        }

        public async Task<bool> UniqueUserName(string username, CancellationToken cancellationToken)
        {
            var user = await _auth.GetUserByIdentifier(username);
            return (user == null) ? true : false;
        }

        public bool MustBeSame(string password, string repeatPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
                return false;

            if (password != repeatPassword)
                return false;

            return true;
        }
    }
}
