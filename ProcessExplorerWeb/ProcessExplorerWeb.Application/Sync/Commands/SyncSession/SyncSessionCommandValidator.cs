using FluentValidation;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    public class SyncSessionCommandValidator : AbstractValidator<SyncSessionCommand>
    {
        public SyncSessionCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty();
            RuleFor(p => p.SessionId).NotEmpty();
            RuleFor(p => p.Started).NotEmpty();

            //lists
            RuleFor(p => p.Processes).NotNull();
            RuleFor(p => p.Applications).NotNull();

            RuleForEach(p => p.Processes).SetValidator(new SynSessionProcessInfoCommandValidator());
            RuleForEach(p => p.Applications).SetValidator(new SynSessionApplicationInfoCommandValidator());
        }
    }

    public class SynSessionApplicationInfoCommandValidator : AbstractValidator<SynSessionApplicationInfoCommand>
    {
        public SynSessionApplicationInfoCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Started).NotEmpty();
        }
    }

    public class SynSessionProcessInfoCommandValidator : AbstractValidator<SynSessionProcessInfoCommand>
    {
        public SynSessionProcessInfoCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Saved).NotEmpty();
        }
    }
}
