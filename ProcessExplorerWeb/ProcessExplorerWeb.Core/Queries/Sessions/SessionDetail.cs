using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Sessions
{
    public class SessionDetail : IQueryResult
    {
        public Guid SessionId { get; set; }
        public DateTime Started { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public int DifferentProcesses { get; set; }
        public int ApplicationNum { get; set; }
    }
}
