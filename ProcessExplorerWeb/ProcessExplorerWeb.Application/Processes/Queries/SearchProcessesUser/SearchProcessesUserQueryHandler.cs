using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.SearchProcessesUser
{
    public class SearchProcessesUserQueryHandler : IRequestHandler<SearchProcessesUserQuery, ProcessPaginationResponseDto<ProcessSearchResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _pagination;
        private readonly ICurrentUserService _currentUser;

        public SearchProcessesUserQueryHandler(IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> pagination,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _pagination = pagination.Value;
            _currentUser = currentUser;
        }

        public async Task<ProcessPaginationResponseDto<ProcessSearchResponseDto>> Handle(SearchProcessesUserQuery request, CancellationToken cancellationToken)
        {
            //Get total number of sessions
            var totalNumberOfSessions = await _unitOfWork.Session.GetNumberOfSessinsForUser(_currentUser.UserId);

            //get paginated records and total nubmer of records
            var (records, count) = await _unitOfWork.Process.GetProcessesForUser(_currentUser.UserId, request.CurrentPage, _pagination.Take, request.SearchCriteria);

            //map
            var dto = new ProcessPaginationResponseDto<ProcessSearchResponseDto>
            {
                TotalRecords = count,
                TotalNumberOfSessions = totalNumberOfSessions,
                Records = records?.Adapt<IEnumerable<ProcessSearchResponseDto>>()
            };

            //success
            return dto;
        }
    }
}
