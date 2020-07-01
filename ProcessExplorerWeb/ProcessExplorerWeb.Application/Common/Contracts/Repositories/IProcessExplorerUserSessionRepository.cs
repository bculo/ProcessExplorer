using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Queries.Charts;
using ProcessExplorerWeb.Core.Queries.Sessions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IProcessExplorerUserSessionRepository : IRepository<ProcessExplorerUserSession>
    {
        /// <summary>
        /// Get number of created session for given period
        /// </summary>
        /// <param name="startOfPeriod"></param>
        /// <param name="endOfPeriod"></param>
        /// <returns></returns>
        Task<int> GetNumberOfSessinsForPeriod(DateTime startOfPeriod, DateTime endOfPeriod);

        /// <summary>
        /// Get number of ssesion created by specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetNumberOfSessinsForUser(Guid userId);

        /// <summary>
        /// SYNC PURPOSE ONLY !!!
        /// Get session with all applications and all processes
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ProcessExplorerUserSession> GetSessionWithAppsAndProcesses(Guid sessionId, Guid userId);

        /// <summary>
        /// SYNC PURPOSE ONLY !!!
        /// Get session with all processes
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ProcessExplorerUserSession> GetSessionWithProcesses(Guid sessionId, Guid userId);


        /// <summary>
        /// SYNC PURPOSE ONLY !!!
        /// Get session with all applications
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ProcessExplorerUserSession> GetSessionWithApps(Guid sessionId, Guid userId);

        /// <summary>
        /// Pagination for choosen user session
        /// User can specify date of session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="selectedDate"></param>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<(List<SessionDetail>, int)> FilterSessions(Guid userId, DateTime? selectedDate, int page, int take);

        /// <summary>
        /// Get information for selected session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<ProcessExplorerUserSession> GetSelectedSession(Guid userId, Guid sessionId);

        /// <summary>
        /// Get stats for operating system for given period
        /// </summary>
        /// <param name="startOfPeriod"></param>
        /// <param name="endOfPeriod"></param>
        /// <returns></returns>
        Task<List<PieChartItem>> GetOperatingSystemStatistics(DateTime startOfPeriod, DateTime endOfPeriod);

        /// <summary>
        /// Get Day with most created sessions in specific period
        /// </summary>
        /// <param name="startOfPeriod"></param>
        /// <param name="endOfPeriod"></param>
        /// <returns></returns>
        Task<TopSessionDay> GetMostActiveDay(DateTime startOfPeriod, DateTime endOfPeriod);

        /// <summary>
        /// Get stats for operating system for specific user
        /// </summary>
        /// <param name="startOfPeriod"></param>
        /// <param name="endOfPeriod"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<PieChartItem>> GetOperatingSystemStatisticsForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId);

        /// <summary>
        /// Get day in which user created most of sessions
        /// </summary>
        /// <param name="startOfPeriod"></param>
        /// <param name="endOfPeriod"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TopSessionDay> GetMostActiveDayForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId);

        /// <summary>
        /// Get chart representation of activity for given period (all users)
        /// </summary>
        /// <param name="startOfPeriod"></param>
        /// <param name="endOfPeriod"></param>
        /// <returns></returns>
        Task<List<SessionDateChartItem>> GetActivityChartStatistics(DateTime startOfPeriod, DateTime endOfPeriod);

        /// <summary>
        /// Get activity chart fo specific user for given period 
        /// </summary>
        /// <param name="startOfPeriod"></param>
        /// <param name="endOfPeriod"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SessionDateChartItem>> GetActivityChartStatisticsForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId);
    }
}
