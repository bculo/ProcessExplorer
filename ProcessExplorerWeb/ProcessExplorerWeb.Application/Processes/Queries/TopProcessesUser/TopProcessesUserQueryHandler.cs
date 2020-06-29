using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.TopProcessesUser
{
    public class TopProcessesUserQueryHandler : IRequestHandler<TopProcessesUserQuery, TopProcessesUserQueryResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _user;

        public TopProcessesUserQueryHandler(IUnitOfWork unitOfWork,
            ICurrentUserService user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<TopProcessesUserQueryResponseDto> Handle(TopProcessesUserQuery request, CancellationToken cancellationToken)
        {
            //get number of sessions for given user id
            int maxNumOfSessions = await _unitOfWork.Session.GetNumberOfSessinsForUser(_user.UserId);

            //get column chart models
            var columnChart = await _unitOfWork.Process.GetMostUsedProcessesForUser(_user.UserId, 10);

            //map
            var dto = new TopProcessesUserQueryResponseDto
            {
                ChartRecords = columnChart?.Adapt<ColumnChartDto>(),
                MaxNumberOfSessions = maxNumOfSessions
            };

            //success
            return dto;
        }
    }
}
