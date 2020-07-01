using ProcessExplorerWeb.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.ProcessesSesionUserStats
{
    public class UserSessionsStatsQueryResponseDto
    {
        public ColumnChartDto ChartRecords { get; set; }
    }
}
