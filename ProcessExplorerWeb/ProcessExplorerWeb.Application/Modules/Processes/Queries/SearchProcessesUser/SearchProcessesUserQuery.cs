using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;

namespace ProcessExplorerWeb.Application.Processes.Queries.SearchProcessesUser
{
    public class SearchProcessesUserQuery : PaginationRequestDto, IRequest<ProcessPaginationResponseDto<ProcessSearchResponseDto>>
    {
        public string SearchCriteria { get; set; }
    }
}
