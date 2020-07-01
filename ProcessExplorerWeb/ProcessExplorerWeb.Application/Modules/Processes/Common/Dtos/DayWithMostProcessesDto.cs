using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Common.Dtos
{
    public class DayWithMostProcessesDto
    {
        public DateTime Day { get; set; }
        public int TotalNumberOfDifferentProcesses { get; set; }
    }
}
