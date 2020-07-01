using MediatR;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppListUser
{
    public class TopOpenedAppListUserQuery : IRequest<TopOpenedApplicationDto>
    {
    }
}
