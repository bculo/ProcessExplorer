using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsPeriod
{
    public class SessionStatisticsPeriodResponseDto
    {
        public IEnumerable<PieChartDto> PieChartRecords { get; set; }
        public SessionMostActiveDayDto MostActiveDay { get; set; }
        public IEnumerable<SessionLineChartDto> ActivityChartRecords { get; set; }
        public DateTime StartOfPeriod { get; set; }
        public DateTime EndOfPeriod { get; set; }
    }
}
