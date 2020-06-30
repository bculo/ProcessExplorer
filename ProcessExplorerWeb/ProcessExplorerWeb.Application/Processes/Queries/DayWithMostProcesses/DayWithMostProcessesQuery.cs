using MediatR;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses
{
    /// <summary>
    /// Get day with most different processes and total number of processes for that day
    /// </summary>
    public class DayWithMostProcessesQuery : IRequest<DayWithMostProcessesDto>
    {
    }
}
