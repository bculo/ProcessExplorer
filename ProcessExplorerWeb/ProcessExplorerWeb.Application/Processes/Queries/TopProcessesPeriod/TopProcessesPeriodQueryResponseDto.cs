using ProcessExplorerWeb.Application.Common.Dtos;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Processes.Queries.TopProcessesPeriod
{
    public class TopProcessesPeriodQueryResponseDto
    {
        public IEnumerable<ColumnChartDto> ChartRecords { get; set; }
    }
}
