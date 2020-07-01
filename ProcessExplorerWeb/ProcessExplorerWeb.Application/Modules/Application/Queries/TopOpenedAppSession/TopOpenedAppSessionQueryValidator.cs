using FluentValidation;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppSession
{
    public class TopOpenedAppSessionQueryValidator : AbstractValidator<TopOpenedAppSessionQuery>
    {
        public TopOpenedAppSessionQueryValidator()
        {
            RuleFor(p => p.SessionId).NotEmpty();
        }
    }
}
