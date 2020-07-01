using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.DayWithMostAppsUser
{
    public class DayWithMostAppsUserQueryHandler : IRequestHandler<DayWithMostAppsUserQuery, DayWithMostAppsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DayWithMostAppsUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<DayWithMostAppsDto> Handle(DayWithMostAppsUserQuery request, CancellationToken cancellationToken)
        {
            //get day with most processes
            var topDay = await _unitOfWork.Applications.DayWithMostOpenedAppsAllTime(_currentUser.UserId);

            //map to dto
            var dto = topDay.Adapt<DayWithMostAppsDto>();

            //sucsess
            return dto;
        }
    }
}
