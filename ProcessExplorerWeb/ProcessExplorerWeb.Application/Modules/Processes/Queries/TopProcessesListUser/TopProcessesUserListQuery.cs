using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Queries.TopProcessesUser
{
    /// <summary>
    /// Most used processes for specific user
    /// </summary>
    public class TopProcessesUserListQuery : IRequest<TopProcessesUserListQueryResponseDto>
    {
    }
}
