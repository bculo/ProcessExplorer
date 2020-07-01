using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppListPeriod
{
    public class TopOpenedAppListPeriodQueryHandler : IRequestHandler<TopOpenedAppListPeriodQuery, TopOpenedApplicationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _date;
        private readonly DateTimePeriodOptions _periodOptions;
        private readonly PaginationOptions _pagination;

        public TopOpenedAppListPeriodQueryHandler(IUnitOfWork unitOfWork,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options,
            IOptions<PaginationOptions> pagination)
        {
            _unitOfWork = unitOfWork;
            _date = date;
            _periodOptions = options.Value;
            _pagination = pagination.Value;
        }

        public async Task<TopOpenedApplicationDto> Handle(TopOpenedAppListPeriodQuery request, CancellationToken cancellationToken)
        {
            //NOTE _periodOptions.DaysBack is negative number. You can see concrete value in appsetings.json
            DateTime endOfPeriod = _date.Now.Date;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack);

            //get column chart models
            var columnChart = await _unitOfWork.Applications.GetMostUsedApplicationsForPeriod(startOfPeriod, endOfPeriod, _pagination.Take);

            //map
            var dto = new TopOpenedApplicationDto
            {
                ChartRecords = columnChart?.Adapt<ColumnChartDto>(),
            };

            //success
            return dto;
        }
    }
}
