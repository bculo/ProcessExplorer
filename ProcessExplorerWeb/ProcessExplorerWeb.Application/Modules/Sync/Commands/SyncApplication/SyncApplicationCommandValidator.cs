using FluentValidation;
using ProcessExplorerWeb.Application.Sync.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncApplication
{
    public class SyncApplicationCommandValidator : AbstractValidator<SyncApplicationCommand>
    {
        public SyncApplicationCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty();
            RuleFor(p => p.SessionId).NotEmpty();
            RuleFor(p => p.Started).NotEmpty();
            RuleFor(p => p.OS).NotEmpty();

            //lists
            RuleForEach(p => p.Applications).SetValidator(new ApplicationInstanceDtoValidator());
        }
    }
}
