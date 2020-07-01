using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Charts
{
    public class PieChartItem : IQueryResult
    {
        public string ChunkName { get; set; }
        public int Quantity { get; set; }
    }
}
