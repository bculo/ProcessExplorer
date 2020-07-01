using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Sessions
{
    public class SessionDateChartItem : IQueryResult
    {
        /// <summary>
        /// X
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Y -> Number of sessions for day
        /// </summary>
        public int Number { get; set; }
    }
}
