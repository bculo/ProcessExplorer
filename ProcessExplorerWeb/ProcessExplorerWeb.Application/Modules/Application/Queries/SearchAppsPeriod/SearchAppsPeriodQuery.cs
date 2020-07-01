using MediatR;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchAppsPeriod
{
    public class SearchAppsPeriodQuery : PaginationRequestDto, IRequest<PaginationResponseDto<AppSearchItemResponseDto>>
    {
        public string SearchCriteria { get; set; }
    }
}
