using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;

namespace ProcessExplorerWeb.Application.Processes.Queries.GetProcessesPeriod
{
    public class GetProcessesPeriodQuery : PaginationRequestDto, IRequest<ProcessPaginationResponseDto<ProcessSearchResponseDto>>
    {
        public string SearchCriteria { get; set; }
    }
}
