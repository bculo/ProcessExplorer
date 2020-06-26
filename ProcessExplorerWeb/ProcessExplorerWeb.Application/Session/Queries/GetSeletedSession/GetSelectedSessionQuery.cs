using MediatR;
using System;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSeletedSession
{
    public class GetSelectedSessionQuery : IRequest<GetSelectedSessionQueryResponseDto>
    {
        public Guid SelectedSessionId { get; set; }
    }
}
