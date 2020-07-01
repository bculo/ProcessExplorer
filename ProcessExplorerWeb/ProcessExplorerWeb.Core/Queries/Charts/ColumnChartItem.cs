using ProcessExplorerWeb.Core.Interfaces;

namespace ProcessExplorerWeb.Core.Queries.Charts
{
    public class ColumnChartItem : IQueryResult
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
