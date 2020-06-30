using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.TopProcessesPeriod
{
    /// <summary>
    /// Most used processes by all users for given period ~ 1 month
    /// </summary>
    public class TopProcessesPeriodListQueryHandler : IRequestHandler<TopProcessesPeriodListQuery, TopProcessesPeriodListQueryResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _date;
        private readonly DateTimePeriodOptions _periodOptions;
        private readonly PaginationOptions _pagination;

        public TopProcessesPeriodListQueryHandler(IUnitOfWork unitOfWork,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options,
            IOptions<PaginationOptions> pagination)
        {
            _unitOfWork = unitOfWork;
            _date = date;
            _periodOptions = options.Value;
            _pagination = pagination.Value;
        }

        public async Task<TopProcessesPeriodListQueryResponseDto> Handle(TopProcessesPeriodListQuery request, CancellationToken cancellationToken)
        {
            //NOTE _periodOptions.DaysBack is negative number. You can see concrete value in appsetings.json
            DateTime endOfPeriod = _date.Now.Date;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack);

            //get number of sessions for given period
            int maxNumOfSessions = await _unitOfWork.Session.GetNumberOfSessinsForPeriod(startOfPeriod, endOfPeriod);

            //get column chart models
            var columnChart = await _unitOfWork.Process.GetMostUsedProcessesForPeriod(startOfPeriod, endOfPeriod, _pagination.Take);

            //map
            var dto = new TopProcessesPeriodListQueryResponseDto
            {
                ChartRecords = columnChart?.Adapt<ColumnChartDto>(),
                MaxNumberOfSessions = maxNumOfSessions
            };

            //success
            return dto;
        }
    }
}
