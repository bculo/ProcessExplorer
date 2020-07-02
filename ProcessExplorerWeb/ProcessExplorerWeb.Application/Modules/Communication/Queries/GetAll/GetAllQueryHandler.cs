using MediatR;
using ProcessExplorerWeb.Application.Common.Contracts.Services;
using ProcessExplorerWeb.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Communication.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetAllQueryResponseDto>
    {
        private readonly ICommunicationTypeService _service;

        public GetAllQueryHandler(ICommunicationTypeService service)
        {
            _service = service;
        }

        public Task<GetAllQueryResponseDto> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var currentCommunication = _service.GetCommunicationType();
            var possibleCommunicationTypes = new List<CommunicationInstanceType>();

            foreach (CommunicationType enumValue in Enum.GetValues(typeof(CommunicationType)))
            {
                possibleCommunicationTypes.Add(new CommunicationInstanceType
                {
                    Name = enumValue.ToString(),
                    IsActive = enumValue == currentCommunication,
                    Type = (int)enumValue
                });
            }

            var dto = new GetAllQueryResponseDto
            {
                Types = possibleCommunicationTypes
            };

            return Task.FromResult(dto);
        }
    }
}
