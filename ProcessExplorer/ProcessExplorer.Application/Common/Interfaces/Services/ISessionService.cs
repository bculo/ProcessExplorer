using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Core.Enums;
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
