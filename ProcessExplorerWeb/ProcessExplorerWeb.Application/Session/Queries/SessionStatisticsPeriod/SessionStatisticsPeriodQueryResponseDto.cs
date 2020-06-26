using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsPeriod
{
    public class SessionStatisticsAllQueryResponseDto
    {
        public IEnumerable<PieChartDto> PieChartRecords { get; set; }
        public SessionMostActiveDayDto MostActiveDay { get; set; }
    }
}
