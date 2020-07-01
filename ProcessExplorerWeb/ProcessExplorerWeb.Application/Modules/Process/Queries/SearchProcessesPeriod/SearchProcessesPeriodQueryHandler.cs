using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.SearchProcessesPeriod
{
    public class SearchProcessesPeriodQueryHandler : IRequestHandler<SearchProcessesPeriodQuery, ProcessPaginationResponseDto<ProcessSearchResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _date;
        private readonly DateTimePeriodOptions _periodOptions;
        private readonly PaginationOptions _pagination;

        public SearchProcessesPeriodQueryHandler(IUnitOfWork unitOfWork,
            IDateTime date,
            IOptions<DateTimePeriodOptions> options,
            IOptions<PaginationOptions> pagination)
        {
            _unitOfWork = unitOfWork;
            _date = date;
            _periodOptions = options.Value;
            _pagination = pagination.Value;
        }

        public async Task<ProcessPaginationResponseDto<ProcessSearchResponseDto>> Handle(SearchProcessesPeriodQuery request, CancellationToken cancellationToken)
        {
            //NOTE _periodOptions.DaysBack is negative number. You can see concrete value in appsetings.json
            DateTime endOfPeriod = _date.Now.Date;
            DateTime startOfPeriod = endOfPeriod.AddDays(_periodOptions.DaysBack);

            //Get total number of sessions
            var totalNumberOfSessions = await _unitOfWork.Session.GetNumberOfSessinsForPeriod(startOfPeriod, endOfPeriod);

            //get paginated records and total nubmer of records
            var (records, count) = await _unitOfWork.Process.GetProcessesForPeriod(startOfPeriod, endOfPeriod, request.CurrentPage, _pagination.Take, request.SearchCriteria);

            //map
            var dto = new ProcessPaginationResponseDto<ProcessSearchResponseDto>
            {
                TotalRecords = count,
                TotalNumberOfSessions = totalNumberOfSessions,
                Records = records?.Adapt<IEnumerable<ProcessSearchResponseDto>>(),
                TotalPages = (int)Math.Ceiling((double)count / _pagination.Take)
            };

            //success
            return dto;
        }
    }
}
