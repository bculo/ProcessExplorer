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

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.OSStatsUser
{
    public class OSStatsUserQueryHandler : IRequestHandler<OSStatsUserQuery, OSStatsUserQueryResponseDto>
    {
        private readonly IUnitOfWork _work;
        private readonly IDateTime _date;
        private readonly ICurrentUserService _currentUser;
        private readonly DateTimePeriodOptions _periodOptions;

        public OSStatsUserQueryHandler(IUnitOfWork work,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options,
            ICurrentUserService currentUser)
        {
            _work = work;
            _date = date;
            _periodOptions = options.Value;
            _currentUser = currentUser;
        }

        public async Task<OSStatsUserQueryResponseDto> Handle(OSStatsUserQuery request, CancellationToken cancellationToken)
        {
            //NOTE _periodOptions.DaysBack is negative number. You can see concrete value in appsetings.json
            DateTime endOfPeriod = _date.Now.Date;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack);

            //get pie chart
            var pieChart = await _work.Applications.GetApplicationStatsForOSUser(startOfPeriod, endOfPeriod, _currentUser.UserId);

            //map to dto instnace
            var dto = new OSStatsUserQueryResponseDto
            {
                PieChart = pieChart?.Adapt<PieChartDto>()
            };

            //sucess
            return dto;
        }
    }
}
