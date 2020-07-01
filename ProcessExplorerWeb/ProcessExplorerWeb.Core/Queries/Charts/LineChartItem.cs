using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Charts
{
    public class LineChartItem : IQueryResult
    {
        /// <summary>
        /// X point
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Y point
        /// </summary>
        public int Value { get; set; }
    }
}
