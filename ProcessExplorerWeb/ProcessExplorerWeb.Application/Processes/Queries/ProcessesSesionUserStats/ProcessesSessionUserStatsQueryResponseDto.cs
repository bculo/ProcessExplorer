﻿using ProcessExplorerWeb.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.ProcessesSesionUserStats
{
    public class ProcessesSessionUserStatsQueryResponseDto
    {
        public IEnumerable<ColumnChartDto> ChartRecords { get; set; }
    }
}