using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSeletedSession
{
    public class GetSelectedSessionQueryHandler : IRequestHandler<GetSelectedSessionQuery, GetSelectedSessionQueryResponseDto>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public GetSelectedSessionQueryHandler(ICurrentUserService currentUser,
            IUnitOfWork unitOfWork)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetSelectedSessionQueryResponseDto> Handle(GetSelectedSessionQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Session.GetSelectedSession(_currentUser.UserId, request.SelectedSessionId);

            var dto = result?.Adapt<GetSelectedSessionQueryResponseDto>();

            return dto;
        }
    }
}
