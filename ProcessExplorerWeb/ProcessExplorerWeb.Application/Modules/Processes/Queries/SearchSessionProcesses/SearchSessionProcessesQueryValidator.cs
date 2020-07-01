using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ProcessExplorerWeb.Application.Processes.Queries.SearchSessionProcesses
{
    public class SearchSessionProcessesQueryValidator : AbstractValidator<SearchSessionProcessesQuery>
    {
        public SearchSessionProcessesQueryValidator()
        {
            RuleFor(p => p.SessionId).NotEmpty();
        }
    }
}
