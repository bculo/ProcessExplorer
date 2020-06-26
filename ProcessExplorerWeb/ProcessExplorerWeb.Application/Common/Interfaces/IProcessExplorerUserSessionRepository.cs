using ProcessExplorerWeb.Application.Common.Charts.Shared;
using ProcessExplorerWeb.Application.Common.Models.Session;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IProcessExplorerUserSessionRepository : IRepository<ProcessExplorerUserSession>
    {
        Task<ProcessExplorerUserSession> GetSessionWithAppsAndProcesses(Guid sessionId, Guid userId);
        Task<ProcessExplorerUserSession> GetSessionWithProcesses(Guid sessionId, Guid userId);
        Task<ProcessExplorerUserSession> GetSessionWithApps(Guid sessionId, Guid userId);
        Task<(List<SessionDetailedModel>, int)> FilterSessions(Guid userId, DateTime? selectedDate, int page, int take);
        Task<ProcessExplorerUserSession> GetSelectedSession(Guid userId, Guid sessionId);
        Task<List<PieChartStatisticModel>> GetOperatingSystemStatistics(DateTime startOfPeriod, DateTime endOfPeriod);
        Task<PopularSessionDayModel> GetMostActiveDay(DateTime startOfPeriod, DateTime endOfPeriod);
        Task<List<PieChartStatisticModel>> GetOperatingSystemStatisticsForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId);
        Task<PopularSessionDayModel> GetMostActiveDayForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId);
        Task<List<SessionChartLineModel>> GetActivityChartStatistics(DateTime startOfPeriod, DateTime endOfPeriod);
        Task<List<SessionChartLineModel>> GetActivityChartStatisticsForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId);
    }
}
