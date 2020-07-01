using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchAppsUser
{
    public class SearchAppsUserQueryHandler : IRequestHandler<SearchAppsUserQuery, PaginationResponseDto<AppSearchItemResponseDto>>
    {
        private readonly PaginationOptions _pagination;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public SearchAppsUserQueryHandler(IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> pagination,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _pagination = pagination.Value;
            _currentUser = currentUser;
        }

        public async Task<PaginationResponseDto<AppSearchItemResponseDto>> Handle(SearchAppsUserQuery request, CancellationToken cancellationToken)
        {
            //Get total number of sessions
            var totalNumberOfSessions = await _unitOfWork.Session.GetNumberOfSessinsForUser(_currentUser.UserId);

            //get paginated records and total nubmer of records
            var (records, count) = await _unitOfWork.Applications.GetAppsForUser(_currentUser.UserId, request.CurrentPage, _pagination.Take, request.SearchCriteria);

            //map
            var dto = new PaginationResponseDto<AppSearchItemResponseDto>
            {
                TotalRecords = count,
                Records = records?.Adapt<IEnumerable<AppSearchItemResponseDto>>(),
                TotalPages = (int)Math.Ceiling((double)count / _pagination.Take)
            };

            //success
            return dto;
        }
    }
}
