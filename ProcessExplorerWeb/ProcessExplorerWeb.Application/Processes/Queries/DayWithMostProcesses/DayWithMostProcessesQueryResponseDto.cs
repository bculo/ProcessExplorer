using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses
{
    public class DayWithMostProcessesQueryResponseDto
    {
        public DateTime Day { get; set; }
        public int TotalNumberOfDifferentProcesses { get; set; }
    }
}
