using ProcessExplorerWeb.Application.Common.Dtos;

namespace ProcessExplorerWeb.Application.Processes.Common.Dtos
{
    public class ProcessPaginationResponseDto<T> : PaginationResponseDto<T> 
    {
        public int TotalNumberOfSessions { get; set; }
    }
}
