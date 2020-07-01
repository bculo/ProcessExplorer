using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses
{
    /// <summary>
    /// Get day with most different processes and total number of processes for that day
    /// </summary>
    public class DayWithMostProcessesQueryHandler : IRequestHandler<DayWithMostProcessesQuery, DayWithMostProcessesDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayWithMostProcessesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<DayWithMostProcessesDto> Handle(DayWithMostProcessesQuery request, CancellationToken cancellationToken)
        {
            //get day with most processes
            var topDay = await _unitOfWork.Process.DayWithMostDifferentProcessesForAllTime();

            //map to dto
            var dto = topDay.Adapt<DayWithMostProcessesDto>();

            //sucsess
            return dto;
        }
    }
}
