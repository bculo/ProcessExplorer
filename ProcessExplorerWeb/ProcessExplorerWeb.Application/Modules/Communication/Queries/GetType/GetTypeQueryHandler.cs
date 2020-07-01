using MediatR;
using ProcessExplorerWeb.Application.Common.Contracts.Services;
using ProcessExplorerWeb.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Communication.Queries.GetType
{
    public class GetTypeQueryHandler : IRequestHandler<GetTypeQuery, GetTypeQueryResponseDto>
    {
        private readonly ICommunicationTypeService _service;

        public GetTypeQueryHandler(ICommunicationTypeService service)
        {
            _service = service;
        }

        public Task<GetTypeQueryResponseDto> Handle(GetTypeQuery request, CancellationToken cancellationToken)
        {
            CommunicationType type = _service.GetCommunicationType();

            return Task.FromResult(new GetTypeQueryResponseDto
            {
                Type = (int)type
            });
        }
    }
}
