using ProcessExplorer.Application.Common.Models;
using System;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ISessionService
    {
        SessionInformation SessionInformation { get; }
    }
}
