using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcessesUser
{
    public class DayWithMostProcessesUserQueryHandler : IRequestHandler<DayWithMostProcessesUserQuery, DayWithMostProcessesDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DayWithMostProcessesUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<DayWithMostProcessesDto> Handle(DayWithMostProcessesUserQuery request, CancellationToken cancellationToken)
        {
            //get day with most processes
            var topDay = await _unitOfWork.Process.DayWithMostDifferentProcessesForAllTime(_currentUser.UserId);

            //map to dto
            var dto = topDay.Adapt<DayWithMostProcessesDto>();

            //sucsess
            return dto;
        }
    }
}
