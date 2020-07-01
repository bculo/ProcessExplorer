using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchSessionsApps
{
    public class SearchSessionsAppsQueryValidator : AbstractValidator<SearchSessionsAppsQuery>
    {
        public SearchSessionsAppsQueryValidator()
        {
            RuleFor(p => p.SessionId).NotEmpty();
        }
    }
}
