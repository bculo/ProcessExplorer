using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.DayWithMostApps
{
    public class DayWithMostAppsQueryHandler : IRequestHandler<DayWithMostAppsQuery, DayWithMostAppsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayWithMostAppsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DayWithMostAppsDto> Handle(DayWithMostAppsQuery request, CancellationToken cancellationToken)
        {
            //get day with most processes
            var topDay = await _unitOfWork.Applications.DayWithMostOpenedAppsAllTime();

            //map to dto
            var dto = topDay.Adapt<DayWithMostAppsDto>();

            //sucsess
            return dto;
        }
    }
}
