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

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchSessionsApps
{
    public class SearchSessionsAppsQueryHandler : IRequestHandler<SearchSessionsAppsQuery, PaginationResponseDto<SearchSessionsAppsQueryResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _pagination;
        private readonly ICurrentUserService _currentUser;

        public SearchSessionsAppsQueryHandler(IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> pagination,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _pagination = pagination.Value;
            _currentUser = currentUser;
        }

        public async Task<PaginationResponseDto<SearchSessionsAppsQueryResponseDto>> Handle(SearchSessionsAppsQuery request, CancellationToken cancellationToken)
        {
            //get paginated records and total nubmer of records
            var (records, count) = await _unitOfWork.Applications.GetApplicationsForUserSession(_currentUser.UserId, request.CurrentPage, _pagination.Take, request.SearchCriteria, request.SessionId);

            //map
            var dto = new PaginationResponseDto<SearchSessionsAppsQueryResponseDto>
            {
                TotalRecords = count,
                Records = records?.Adapt<IEnumerable<SearchSessionsAppsQueryResponseDto>>(),
                TotalPages = (int)Math.Ceiling((double)count / _pagination.Take)
            };

            //success
            return dto;
        }
    }
}
