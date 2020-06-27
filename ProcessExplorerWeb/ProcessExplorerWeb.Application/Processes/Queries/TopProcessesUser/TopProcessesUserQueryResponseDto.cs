using ProcessExplorerWeb.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.TopProcessesUser
{
    public class TopProcessesUserQueryResponseDto
    {
        public IEnumerable<ColumnChartDto> ChartRecords { get; set; }
        public int MaxNumberOfSessions { get; set; }
    }
}
