using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses
{
    public class DayWithMostProcessesQueryHandler : IRequestHandler<DayWithMostProcessesQuery, DayWithMostProcessesQueryResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayWithMostProcessesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<DayWithMostProcessesQueryResponseDto> Handle(DayWithMostProcessesQuery request, CancellationToken cancellationToken)
        {
            //get day with most processes
            var topDay = await _unitOfWork.Process.DayWithMostDifferentProcessesForAllTime();

            //map to dto
            var dto = topDay.Adapt<DayWithMostProcessesQueryResponseDto>();

            //sucsess
            return dto;
        }
    }
}
