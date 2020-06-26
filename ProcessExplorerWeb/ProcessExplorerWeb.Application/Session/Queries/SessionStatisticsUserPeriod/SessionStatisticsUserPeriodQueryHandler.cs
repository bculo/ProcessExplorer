using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Session.Extensions;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsUserPeriod
{
    public class SessionStatisticsUserPeriodQueryHandler : IRequestHandler<SessionStatisticsUserPeriodQuery, SessionStatisticsUserPeriodQueryResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _date;
        private readonly DateTimePeriodOptions _periodOptions;

        public SessionStatisticsUserPeriodQueryHandler(IUnitOfWork unitOfWork,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _date = date;
            _periodOptions = options.Value;
            _currentUser = currentUser;
        }

        public async Task<SessionStatisticsUserPeriodQueryResponseDto> Handle(SessionStatisticsUserPeriodQuery request, CancellationToken cancellationToken)
        {
            //NOTE _periodOptions.DaysBack is negative number. You can see concrete value in appsetings.json
            DateTime endOfPeriod = _date.Now.Date;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack);

            //pie chart statistis for operating systems
            var piceChartStatistics = await _unitOfWork.Session.GetOperatingSystemStatisticsForUser(startOfPeriod, endOfPeriod, _currentUser.UserId);

            //get statistics for most active day
            var mostActiveDay = await _unitOfWork.Session.GetMostActiveDayForUser(startOfPeriod, endOfPeriod, _currentUser.UserId);

            //get line chart for activity
            var lineChartStatistic = await _unitOfWork.Session.GetActivityChartStatisticsForUser(startOfPeriod, endOfPeriod, _currentUser.UserId);
            //fill mising dates
            lineChartStatistic.FillDatesThatAreMissing(startOfPeriod, endOfPeriod);

            //map to dto
            var finalDto = new SessionStatisticsUserPeriodQueryResponseDto
            {
                MostActiveDay = mostActiveDay?.Adapt<SessionMostActiveDayDto>(),
                PieChartRecords = piceChartStatistics?.Adapt<IEnumerable<PieChartDto>>(),
                ActivityChartRecords = lineChartStatistic?.Adapt<IEnumerable<SessionLineChartDto>>(),
                EndOfPeriod = endOfPeriod,
                StartOfPeriod = startOfPeriod
            };

            return finalDto;
        }
    }
}
