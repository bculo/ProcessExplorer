using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchSessionsApps
{
    public class SearchSessionsAppsQuery : PaginationRequestDto, IRequest<PaginationResponseDto<SearchSessionsAppsQueryResponseDto>>
    {
        public string SearchCriteria { get; set; }
        public Guid SessionId { get; set; }
    }
}
