using MediatR;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppListPeriod
{
    public class TopOpenedAppListPeriodQuery : IRequest<TopOpenedApplicationDto>
    {
    }
}
