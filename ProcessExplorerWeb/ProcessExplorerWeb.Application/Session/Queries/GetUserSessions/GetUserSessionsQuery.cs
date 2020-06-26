using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using System;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSessions
{
    public class GetUserSessionsQuery : PaginationRequestDto, IRequest<PaginationResponseDto<GetUserSessionsQueryResponseDto>>
    {
        public DateTime? SessionDate { get; set; }
    }
}
