using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsPeriod
{
    public class SessionStatisticsAllQuery : IRequest<SessionStatisticsAllQueryResponseDto>
    {
    }
}
