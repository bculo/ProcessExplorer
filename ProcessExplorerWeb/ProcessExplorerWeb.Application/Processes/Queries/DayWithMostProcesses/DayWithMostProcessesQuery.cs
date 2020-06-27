using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses
{
    public class DayWithMostProcessesQuery : IRequest<DayWithMostProcessesQueryResponseDto>
    {
    }
}
