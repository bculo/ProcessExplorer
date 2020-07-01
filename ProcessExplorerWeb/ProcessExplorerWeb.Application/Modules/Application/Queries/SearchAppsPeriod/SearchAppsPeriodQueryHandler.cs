using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchAppsPeriod
{
    public class SearchAppsPeriodQueryHandler : IRequestHandler<SearchAppsPeriodQuery, PaginationResponseDto<AppSearchItemResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _date;
        private readonly DateTimePeriodOptions _periodOptions;
        private readonly PaginationOptions _pagination;

        public SearchAppsPeriodQueryHandler(IUnitOfWork unitOfWork,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options,
            IOptions<PaginationOptions> pagination)
        {
            _unitOfWork = unitOfWork;
            _date = date;
            _periodOptions = options.Value;
            _pagination = pagination.Value;
        }

        public async Task<PaginationResponseDto<AppSearchItemResponseDto>> Handle(SearchAppsPeriodQuery request, CancellationToken cancellationToken)
        {
            DateTime endOfPeriod = _date.Now.Date;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack);

            //get paginated records and total nubmer of records
            var (records, count) = await _unitOfWork.Applications.GetAppsForPeriod(startOfPeriod, endOfPeriod, request.CurrentPage, _pagination.Take, request.SearchCriteria);

            //map
            var dto = new PaginationResponseDto<AppSearchItemResponseDto>
            {
                TotalRecords = count,
                Records = records?.Adapt<IEnumerable<AppSearchItemResponseDto>>(),
                TotalPages = (int)Math.Ceiling((double)count / _pagination.Take)
            };

            //success
            return dto;
        }
    }
}
