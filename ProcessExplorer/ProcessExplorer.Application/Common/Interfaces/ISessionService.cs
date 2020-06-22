using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Models;
using System;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ISessionService
    {
        SessionInformation SessionInformation { get; }

        void ChangeSessionId(Guid sessionId);
        void SetMode(WorkMode mode);
    }
}
