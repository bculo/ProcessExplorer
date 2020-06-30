using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.TopProcessesPeriod
{
    /// <summary>
    /// Most used processes by all users for given period ~ 1 month
    /// </summary>
    public class TopProcessesPeriodListQuery : IRequest<TopProcessesPeriodListQueryResponseDto>
    {
    }
}
