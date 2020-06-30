using MediatR;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcessesUser
{
    public class DayWithMostProcessesUserQuery : IRequest<DayWithMostProcessesDto>
    {
    }
}
