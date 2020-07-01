using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Communication.Commands.ChangeType
{
    public class ChangeTypeCommand : IRequest
    {
        public int Type { get; set; }
    }
}
