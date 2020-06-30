using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.SearchSessionProcesses
{
    public class SearchSessionProcessesQueryHandler : IRequestHandler<SearchSessionProcessesQuery, PaginationResponseDto<ProcessSearchResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _pagination;
        private readonly ICurrentUserService _currentUser;

        public SearchSessionProcessesQueryHandler(IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> pagination,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _pagination = pagination.Value;
            _currentUser = currentUser;
        }

        public async Task<PaginationResponseDto<ProcessSearchResponseDto>> Handle(SearchSessionProcessesQuery request, CancellationToken cancellationToken)
        {
            //get paginated records and total nubmer of records
            var (records, count) = await _unitOfWork.Process.GetProcessesForUserSession(_currentUser.UserId, request.CurrentPage, _pagination.Take, request.SearchCriteria, request.SessionId);

            //map
            var dto = new PaginationResponseDto<ProcessSearchResponseDto>
            {
                TotalRecords = count,
                Records = records?.Adapt<IEnumerable<ProcessSearchResponseDto>>(),
                TotalPages = (int)Math.Ceiling((double)count / _pagination.Take)
            };

            //success
            return dto;
        }
    }
}
