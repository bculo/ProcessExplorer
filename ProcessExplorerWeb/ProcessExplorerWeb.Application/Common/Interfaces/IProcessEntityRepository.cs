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
        Task<List<ProcessEntity>> GetProcessesForSession(Guid sessionId);
        Task<(List<ProcessSearchModel>, int)> GetProcessesForPeriod(DateTime start, DateTime end, int currentPage, int take, string searchCriteria);
        Task<(List<ProcessSearchModel>, int)> GetProcessesForUser(Guid userId, int currentPage, int take, string searchCriteria);
        Task<List<ColumnChartStatisticModel>> GetMostUsedProcessesForPeriod(DateTime start, DateTime end, int take);
        Task<List<ColumnChartStatisticModel>> GetMostUsedProcessesForUser(Guid userId, int take);
    }
}
