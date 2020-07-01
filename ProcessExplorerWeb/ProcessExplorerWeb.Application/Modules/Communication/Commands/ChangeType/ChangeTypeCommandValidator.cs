using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Communication.Commands.ChangeType
{
    public class ChangeTypeCommandValidator : AbstractValidator<ChangeTypeCommand>
    {
        public ChangeTypeCommandValidator()
        {
            RuleFor(i => i.Type).LessThan(5).GreaterThan(0);
        }
    }
}
