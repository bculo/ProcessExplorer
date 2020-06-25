using ProcessExplorerWeb.Application.Common.Models.Session;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IProcessExplorerUserSessionRepository : IRepository<ProcessExplorerUserSession>
    {
        Task<ProcessExplorerUserSession> GetSessionWithAppsAndProcesses(Guid sessionId, Guid userId);
        Task<(List<SessionDetailedModel>, int)> FilterSessions(Guid userId, DateTime? selectedDate, int page, int take);
    }
}
