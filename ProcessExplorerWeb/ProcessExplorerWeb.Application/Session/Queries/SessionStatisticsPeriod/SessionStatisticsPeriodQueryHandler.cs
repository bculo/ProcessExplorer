﻿using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsPeriod
{
    public class SessionStatisticsAllQueryHandler : IRequestHandler<SessionStatisticsPeriodQuery, SessionStatisticsPeriodResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _date;
        private readonly DateTimePeriodOptions _periodOptions;

        public SessionStatisticsAllQueryHandler(IUnitOfWork unitOfWork,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options)
        {
            _unitOfWork = unitOfWork;
            _date = date;
            _periodOptions = options.Value;
        }

        public async Task<SessionStatisticsPeriodResponseDto> Handle(SessionStatisticsPeriodQuery request, CancellationToken cancellationToken)
        {
            //NOTE _periodOptions.DaysBack is negative number. You can see concrete value in appsetings.json
            DateTime endOfPeriod = _date.Now;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack); 

            //pie chart statistis for operating systems
            var piceChartStatistics = await _unitOfWork.Session.OperatingSystemStatistics(startOfPeriod, endOfPeriod);

            //get statistics for most active day
            var mostActiveDay = await _unitOfWork.Session.GetMostActiveDay(startOfPeriod, endOfPeriod);

            //map to dto
            var finalDto = new SessionStatisticsPeriodResponseDto
            {
                MostActiveDay = mostActiveDay?.Adapt<SessionMostActiveDayDto>(),
                PieChartRecords = piceChartStatistics?.Adapt<IEnumerable<PieChartDto>>()
            };

            return finalDto;
        }
    }
}