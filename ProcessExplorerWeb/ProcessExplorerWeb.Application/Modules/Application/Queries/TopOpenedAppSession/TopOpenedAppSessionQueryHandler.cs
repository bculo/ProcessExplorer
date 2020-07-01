using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppSession
{
    public class TopOpenedAppSessionQueryHandler : IRequestHandler<TopOpenedAppSessionQuery, TopOpenedApplicationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _user;

        public TopOpenedAppSessionQueryHandler(IUnitOfWork unitOfWork,
            ICurrentUserService user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<TopOpenedApplicationDto> Handle(TopOpenedAppSessionQuery request, CancellationToken cancellationToken)
        {
            //get column chart models
           var columnChart = await _unitOfWork.Applications.GetMostUsedApplicationsForUserSession(_user.UserId, request.SessionId, 10);

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
