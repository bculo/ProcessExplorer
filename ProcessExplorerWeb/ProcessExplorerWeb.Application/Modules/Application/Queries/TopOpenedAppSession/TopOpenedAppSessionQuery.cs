using MediatR;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppSession
{
    public class TopOpenedAppSessionQuery : IRequest<TopOpenedApplicationDto>
    {
        public Guid SessionId { get; set; }
    }
}
