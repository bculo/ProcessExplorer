using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsUserPeriod
{
    public class SessionStatisticsUserPeriodQueryResponseDto
    {
        public IEnumerable<PieChartDto> PieChartRecords { get; set; }
        public SessionMostActiveDayDto MostActiveDay { get; set; }
    }
}
