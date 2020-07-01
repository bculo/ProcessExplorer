using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Applications
{
    public class AppSessionLineChartItem
    {
        /// <summary>
        /// X -> date when session started
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Y -> Number of opened app in session
        /// </summary>
        public int Number { get; set; }
    }
}
