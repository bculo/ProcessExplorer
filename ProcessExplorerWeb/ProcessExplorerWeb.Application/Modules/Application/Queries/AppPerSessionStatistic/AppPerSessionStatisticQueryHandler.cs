using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.AppPerSessionStatistic
{
    public class AppPerSessionStatisticQueryHandler : IRequestHandler<AppPerSessionStatisticQuery, AppPerSessionStatisticQueryResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _user;

        public AppPerSessionStatisticQueryHandler(IUnitOfWork unitOfWork,
            ICurrentUserService user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<AppPerSessionStatisticQueryResponseDto> Handle(AppPerSessionStatisticQuery request, CancellationToken cancellationToken)
        {
            //get column chart models
            var chartModel = await _unitOfWork.Applications.GetNumberOfAppsLastSessions(_user.UserId, 10);

            //map
            var dto = new AppPerSessionStatisticQueryResponseDto
            {
                Chart = chartModel?.Adapt<AppSessionLineChartDto>(),
            };

            //success
            return dto;
        }
    }
}
