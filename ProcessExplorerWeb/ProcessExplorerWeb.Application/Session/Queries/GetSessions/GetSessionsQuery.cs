using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using System;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSessions
{
    public class GetSessionsQuery : PaginationRequestDto, IRequest<PaginationResponseDto<GetSessionsQueryResponseDto>>
    {
        public DateTime? SessionDate { get; set; }
    }
}
