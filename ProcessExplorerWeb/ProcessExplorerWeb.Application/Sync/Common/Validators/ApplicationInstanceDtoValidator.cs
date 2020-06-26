using FluentValidation;
using ProcessExplorerWeb.Application.Sync.SharedDtos;

namespace ProcessExplorerWeb.Application.Sync.Common.Validators
{
    public class ApplicationInstanceDtoValidator : AbstractValidator<ApplicationInstanceDto>
    {
        public ApplicationInstanceDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Started).NotEmpty();
            RuleFor(x => x.LastUse).NotEmpty();
            RuleFor(x => x.SessionId).NotEmpty();
        }
    }
}
