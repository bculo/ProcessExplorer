using ProcessExplorerWeb.Application.Common.Charts.Shared;
using ProcessExplorerWeb.Application.Common.Models.Charts;
using ProcessExplorerWeb.Application.Common.Models.Process;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IProcessEntityRepository : IRepository<ProcessEntity>
    {
        /// <summary>
        /// Get all processes for given session ID
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<List<ProcessEntity>> GetProcessesForSession(Guid sessionId);

        /// <summary>
        /// Get day with most processes for all time
        /// </summary>
        /// <returns></returns>
        Task<TopProcessDayModel> DayWithMostDifferentProcessesForAllTime();

        /// <summary>
        /// Get day with most processes for given user
        /// </summary>
        /// <returns></returns>
        Task<TopProcessDayModel> DayWithMostDifferentProcessesForAllTime(Guid userId);

        /// <summary>
        /// Search processes for specific session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="take"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        Task<(List<ProcessSearchModel>, int)> GetProcessesForUserSession(Guid userId, int currentPage, int take, string searchCriteria, Guid sessionId);

        /// <summary>
        /// Search all processes for given period
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="currentPage"></param>
        /// <param name="take"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        Task<(List<ProcessSearchModel>, int)> GetProcessesForPeriod(DateTime start, DateTime end, int currentPage, int take, string searchCriteria);

        /// <summary>
        /// Search user processes
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="take"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        Task<(List<ProcessSearchModel>, int)> GetProcessesForUser(Guid userId, int currentPage, int take, string searchCriteria);

        /// <summary>
        /// Get processes that apperas in largest number of sesssions in specific period like 1 month 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<ColumnChartStatisticModel>> GetMostUsedProcessesForPeriod(DateTime start, DateTime end, int take);

        /// <summary>
        /// Get processes that appears in most of user sessions
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<ColumnChartStatisticModel>> GetMostUsedProcessesForUser(Guid userId, int take);

        /// <summary>
        /// For each session get number of different processes
        /// Models are sorted by Start date of session (from newest session to oldest
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<ColumnChartStatisticModel>> GetProcessesStatsForSessions(Guid userId, int take);

        /// <summary>
        /// Get operating system statistic for processes for given period
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<List<PieChartStatisticModel>> GetProcessStatsForOSPeriod(DateTime start, DateTime end);

        /// <summary>
        /// Get operating system statistic for processes for given period and user
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<List<PieChartStatisticModel>> GetProcessStatsForOSUser(DateTime start, DateTime end, Guid userId);
    }
}
