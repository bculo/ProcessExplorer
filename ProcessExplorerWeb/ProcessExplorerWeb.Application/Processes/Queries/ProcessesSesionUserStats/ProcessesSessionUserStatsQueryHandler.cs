using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.ProcessesSesionUserStats
{
    /// <summary>
    /// Gets number of different processes per session ( MAX 20 sessions )
    /// </summary>
    public class ProcessesSessionUserStatsQueryHandler : IRequestHandler<ProcessesSessionUserStatsQuery, ProcessesSessionUserStatsQueryResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _pagination;
        private readonly ICurrentUserService _currentUser;

        public ProcessesSessionUserStatsQueryHandler(IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> pagination,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _pagination = pagination.Value;
            _currentUser = currentUser;
        }

        public async Task<ProcessesSessionUserStatsQueryResponseDto> Handle(ProcessesSessionUserStatsQuery request, CancellationToken cancellationToken)
        {
            var columnChartModels = await _unitOfWork.Process.GetProcessesStatsForSessions(_currentUser.UserId, _pagination.Take);

            //map to dto
            var dto = new ProcessesSessionUserStatsQueryResponseDto
            {
                ChartRecords = columnChartModels?.Adapt<IEnumerable<ColumnChartDto>>(),
            };

            //success
            return dto;
        }
    }
}
