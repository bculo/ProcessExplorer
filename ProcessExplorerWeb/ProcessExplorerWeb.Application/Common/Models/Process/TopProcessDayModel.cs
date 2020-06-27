using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Models.Process
{
    public class TopProcessDayModel
    {
        public DateTime Day { get; set; }
        public int TotalNumberOfDifferentProcesses { get; set; }
    }
}
