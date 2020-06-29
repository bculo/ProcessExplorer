using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSessions
{
    public class GetUserSessionsQueryHandler : IRequestHandler<GetUserSessionsQuery, PaginationResponseDto<GetUserSessionsQueryResponseDto>>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _pagination;

        public GetUserSessionsQueryHandler(ICurrentUserService currentUser, 
            IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> options)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _pagination = options.Value;
        }

        public async Task<PaginationResponseDto<GetUserSessionsQueryResponseDto>> Handle(GetUserSessionsQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUser.UserId;

            var (records, total) = await _unitOfWork.Session.FilterSessions(userId, request.SessionDate, request.CurrentPage, _pagination.Take);

            var finalDto = new PaginationResponseDto<GetUserSessionsQueryResponseDto>
            {
                Records = records?.Adapt<IEnumerable<GetUserSessionsQueryResponseDto>>(),
                TotalRecords = total,
                TotalPages = (int)Math.Ceiling((double)total / _pagination.Take)
            };

            return finalDto;
        }
    }
}
