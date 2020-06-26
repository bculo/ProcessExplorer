using System;

namespace ProcessExplorerWeb.Application.Common.Models.Session
{
    public class SessionChartLineModel
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
