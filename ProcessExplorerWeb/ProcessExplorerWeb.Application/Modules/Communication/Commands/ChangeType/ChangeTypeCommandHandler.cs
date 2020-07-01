using MediatR;
using ProcessExplorerWeb.Application.Common.Contracts.Services;
using ProcessExplorerWeb.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Communication.Commands.ChangeType
{
    public class ChangeTypeCommandHandler : IRequestHandler<ChangeTypeCommand>
    {
        private readonly ICommunicationTypeService _service;

        public ChangeTypeCommandHandler(ICommunicationTypeService service)
        {
            _service = service;
        }

        public Task<Unit> Handle(ChangeTypeCommand request, CancellationToken cancellationToken)
        {
            CommunicationType type = (CommunicationType)Enum.ToObject(typeof(CommunicationType), request.Type);

            _service.SetCommuncationType(type);

            return Task.FromResult(Unit.Value);
        }
    }
}
