using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Processes
{
    public class TopProcessDay : IQueryResult
    {
        public DateTime Day { get; set; }
        public int TotalNumberOfDifferentProcesses { get; set; }
    }
}
