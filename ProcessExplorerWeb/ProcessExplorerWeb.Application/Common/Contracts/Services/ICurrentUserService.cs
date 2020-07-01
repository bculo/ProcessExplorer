using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
