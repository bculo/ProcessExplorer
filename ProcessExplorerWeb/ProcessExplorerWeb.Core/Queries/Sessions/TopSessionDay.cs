using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Sessions
{
    public class TopSessionDay : IQueryResult
    {
        /// <summary>
        /// Day with most sessions
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Number of session
        /// </summary>
        public int NumberOfSessions { get; set; }
    }
}
