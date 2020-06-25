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
    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, PaginationResponseDto<GetSessionsQueryResponseDto>>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _pagination;

        public GetSessionsQueryHandler(ICurrentUserService currentUser, 
            IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> options)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _pagination = options.Value;
        }

        public async Task<PaginationResponseDto<GetSessionsQueryResponseDto>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _currentUser.UserId;

            var (records, total) = await _unitOfWork.Session.FilterSessions(userId, request.SessionDate, request.CurrentPage, _pagination.Take);

            var finalDto = new PaginationResponseDto<GetSessionsQueryResponseDto>
            {
                Records = records?.Adapt<IEnumerable<GetSessionsQueryResponseDto>>(),
                TotalRecords = total
            };

            return finalDto;
        }
    }
}
