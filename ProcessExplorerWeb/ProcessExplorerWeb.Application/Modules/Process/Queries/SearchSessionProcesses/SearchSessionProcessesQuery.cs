using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.SearchSessionProcesses
{
    public class SearchSessionProcessesQuery : PaginationRequestDto, IRequest<PaginationResponseDto<ProcessSearchResponseDto>>
    {
        public string SearchCriteria { get; set; }
        public Guid SessionId { get; set; }
    }
}
