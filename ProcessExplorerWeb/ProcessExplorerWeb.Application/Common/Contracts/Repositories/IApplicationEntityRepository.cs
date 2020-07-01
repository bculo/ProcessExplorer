using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Queries.Applications;
using ProcessExplorerWeb.Core.Queries.Charts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IApplicationEntityRepository : IRepository<ApplicationEntity>
    {
        Task<List<ApplicationEntity>> GetApplicationsForSession(Guid sessionId);

        /// <summary>
        /// Get day with most processes for all time
        /// </summary>
        /// <returns></returns>
        Task<TopApplicationDay> DayWithMostOpenedAppsAllTime();

        Task<TopApplicationDay> DayWithMostOpenedAppsAllTime(Guid userId);


        /// <summary>
        /// Get operating system statistic for apps for given period
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<List<PieChartItem>> GetApplicationStatsForOSPeriod(DateTime start, DateTime end);

        /// <summary>
        /// Get operating system statistic for apps for given period and user
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<List<PieChartItem>> GetApplicationStatsForOSUser(DateTime start, DateTime end, Guid userId);

        /// <summary>
        /// Search all processes for given period
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="currentPage"></param>
        /// <param name="take"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        Task<(List<ApplicationSearchItem>, int)> GetAppsForPeriod(DateTime start, DateTime end, int currentPage, int take, string searchCriteria);

        /// <summary>
        /// Search user processes
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="take"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        Task<(List<ApplicationSearchItem>, int)> GetAppsForUser(Guid userId, int currentPage, int take, string searchCriteria);


        Task<(List<ApplicationSearchDetail>, int)> GetApplicationsForUserSession(Guid userId, int currentPage, int take, string searchCriteria, Guid sessionId);


        /// <summary>
        /// Get processes that apperas in largest number of sesssions in specific period like 1 month 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<ColumnChartItem>> GetMostUsedApplicationsForPeriod(DateTime start, DateTime end, int take);

        /// <summary>
        /// Get processes that appears in most of user sessions
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<ColumnChartItem>> GetMostUsedApplicationsForUser(Guid userId, int take);

        /// <summary>
        /// Get processes that appears most in single user session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<ColumnChartItem>> GetMostUsedApplicationsForUserSession(Guid userId, Guid sessionId, int take);

        /// <summary>
        /// Number of openeds apps per session (taking only last sessions)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<AppSessionLineChartItem>> GetNumberOfAppsLastSessions(Guid userId, int take);
    }
}
