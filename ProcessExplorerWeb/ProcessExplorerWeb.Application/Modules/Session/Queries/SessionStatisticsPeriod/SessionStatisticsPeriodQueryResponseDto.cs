using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsPeriod
{
    public class SessionStatisticsPeriodResponseDto
    {
        public PieChartDto PieChartRecords { get; set; }
        public SessionMostActiveDayDto MostActiveDay { get; set; }
        public SessionLineChartDto ActivityChartRecords { get; set; }
        public int TotalNumberOfSessions { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
