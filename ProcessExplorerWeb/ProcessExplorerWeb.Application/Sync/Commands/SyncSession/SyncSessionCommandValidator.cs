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
            RuleFor(p => p.OS).NotEmpty();
            //RuleFor(p => p.UserId).NotEmpty();

            //lists
            RuleForEach(p => p.Processes).SetValidator(new SynSessionProcessInfoCommandValidator());
            RuleForEach(p => p.Applications).SetValidator(new SynSessionApplicationInfoCommandValidator());
        }
    }

    public class SynSessionApplicationInfoCommandValidator : AbstractValidator<SyncSessionApplicationInfoCommand>
    {
        public SynSessionApplicationInfoCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Started).NotEmpty();
            RuleFor(x => x.LastUse).NotEmpty();
            RuleFor(x => x.SessionId).NotEmpty();
        }
    }

    public class SynSessionProcessInfoCommandValidator : AbstractValidator<SyncSessionProcessInfoCommand>
    {
        public SynSessionProcessInfoCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Detected).NotEmpty();
            RuleFor(x => x.SessionId).NotEmpty();
        }
    }
}
