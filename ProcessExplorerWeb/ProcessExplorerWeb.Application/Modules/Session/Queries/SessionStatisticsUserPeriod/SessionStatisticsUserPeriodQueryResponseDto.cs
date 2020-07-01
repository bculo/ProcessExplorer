using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsUserPeriod
{
    public class SessionStatisticsUserPeriodQueryResponseDto
    {
        public PieChartDto PieChartRecords { get; set; }
        public SessionMostActiveDayDto MostActiveDay { get; set; }
        public SessionLineChartDto ActivityChartRecords { get; set; }
    }
}
