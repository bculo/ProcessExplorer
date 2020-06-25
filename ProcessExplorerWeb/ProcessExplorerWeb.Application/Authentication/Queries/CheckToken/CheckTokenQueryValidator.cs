using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Authentication.Queries.CheckToken
{
    public class CheckTokenQueryValidator : AbstractValidator<CheckTokenQuery>
    {
        public CheckTokenQueryValidator()
        {
            RuleFor(p => p.Token).NotEmpty();
        }
    }
}
