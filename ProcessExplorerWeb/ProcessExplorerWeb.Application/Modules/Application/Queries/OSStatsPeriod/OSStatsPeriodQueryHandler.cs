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

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.OSStatsPeriod
{
    public class OSStatsPeriodQueryHandler : IRequestHandler<OSStatsPeriodQuery, OSStatsPeriodQueryResponseDto>
    {
        private readonly IUnitOfWork _work;
        private readonly IDateTime _date;
        private readonly DateTimePeriodOptions _periodOptions;

        public OSStatsPeriodQueryHandler(IUnitOfWork work,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options)
        {
            _work = work;
            _date = date;
            _periodOptions = options.Value;
        }

        public async Task<OSStatsPeriodQueryResponseDto> Handle(OSStatsPeriodQuery request, CancellationToken cancellationToken)
        {
            //NOTE _periodOptions.DaysBack is negative number. You can see concrete value in appsetings.json
            DateTime endOfPeriod = _date.Now.Date;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack);

            //get pie chart
            var pieChart = await _work.Applications.GetApplicationStatsForOSPeriod(startOfPeriod, endOfPeriod);

            //map to dto instnace
            var dto = new OSStatsPeriodQueryResponseDto
            {
                PieChart = pieChart?.Adapt<PieChartDto>()
            };

            //sucess
            return dto;
        }
    }
}
