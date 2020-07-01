using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Applications
{
    public class ApplicationSearchItem : IQueryResult
    {
        /// <summary>
        /// Name of process
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// occures in number of sessions
        /// </summary>
        public int OccuresNumOfTime { get; set; }
    }
}
