using FluentValidation;
using ProcessExplorerWeb.Application.Sync.Common.Validators;

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

            //lists
            RuleForEach(p => p.Processes).SetValidator(new ProcessInstanceDtoValidator());
            RuleForEach(p => p.Applications).SetValidator(new ApplicationInstanceDtoValidator());
        }
    }
}
