using ProcessExplorerWeb.Application.Common.Dtos;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Processes.Queries.TopProcessesPeriod
{
    public class TopProcessesPeriodListQueryResponseDto
    {
        public ColumnChartDto ChartRecords { get; set; }
        public int MaxNumberOfSessions { get; set; }
    }
}
