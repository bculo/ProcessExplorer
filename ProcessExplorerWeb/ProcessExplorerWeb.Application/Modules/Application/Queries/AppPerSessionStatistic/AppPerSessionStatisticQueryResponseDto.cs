using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.AppPerSessionStatistic
{
    public class AppPerSessionStatisticQueryResponseDto
    {
        public AppSessionLineChartDto Chart { get; set; }
    }
}
