﻿using ProcessExplorerWeb.Core.Entities;
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
    }
}
