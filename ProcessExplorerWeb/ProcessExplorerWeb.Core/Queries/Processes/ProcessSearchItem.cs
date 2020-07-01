using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Processes
{
    public class ProcessSearchItem : IQueryResult
    {
        /// <summary>
        /// Name of process
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// occures in number of sessions
        /// </summary>
        public int OccuresInNumOfSessions { get; set; }
    }
}
