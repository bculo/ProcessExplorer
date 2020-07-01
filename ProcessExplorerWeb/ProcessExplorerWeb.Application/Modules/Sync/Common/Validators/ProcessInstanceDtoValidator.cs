using FluentValidation;
using ProcessExplorerWeb.Application.Sync.SharedDtos;

namespace ProcessExplorerWeb.Application.Sync.Common.Validators
{
    public class ProcessInstanceDtoValidator : AbstractValidator<ProcessInstanceDto>
    {
        public ProcessInstanceDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Detected).NotEmpty();
            RuleFor(x => x.SessionId).NotEmpty();
        }
    }
}
