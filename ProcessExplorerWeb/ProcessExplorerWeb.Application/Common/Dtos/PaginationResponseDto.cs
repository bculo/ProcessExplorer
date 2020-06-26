using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Common.Dtos
{
    public class PaginationResponseDto<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }
        public bool MoreRecordsAvailable { get; set; }
    }
}
