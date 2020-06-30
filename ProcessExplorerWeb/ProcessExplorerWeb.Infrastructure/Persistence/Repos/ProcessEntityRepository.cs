using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Charts.Shared;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Models.Charts;
using ProcessExplorerWeb.Application.Common.Models.Process;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Repos
{
    public class ProcessEntityRepository : Repository<ProcessEntity>, IProcessEntityRepository
    {
        private ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public ProcessEntityRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ProcessEntity>> GetProcessesForSession(Guid sessionId)
        {
            return await ProcessExplorerDbContext.Processes.Where(i => i.SessionId == sessionId)
                                        .ToListAsync();
        }

        public async Task<(List<ProcessSearchModel>, int)> GetProcessesForPeriod(DateTime start, DateTime end, int currentPage, int take, string searchCriteria)
        {
            //get records for specific period and group them by name
            var query = ProcessExplorerDbContext.Processes.Where(i => i.Detected.Date >= start && i.Detected.Date <= end);

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                //search processes using given search criteria
                searchCriteria = searchCriteria.ToLower();
                query = query.Where(i => i.ProcessName.ToLower().Contains(searchCriteria));
            }

            //group by process name
            var groupBy = query.GroupBy(i => i.ProcessName);

            //count total number of records
            int count = await groupBy.Select(i => i.Key).CountAsync();

            //get concrete records (PAGINATED)
            var records = await groupBy.Skip((currentPage - 1) * take).Take(take)
                                        .Select(i => new ProcessSearchModel
                                        {
                                            ProcessName = i.Key,
                                            OccuresInNumOfSessions = i.Count(),
                                        })
                                        .ToListAsync();

            return (records, count);
        }

        public async Task<(List<ProcessSearchModel>, int)> GetProcessesForUser(Guid userId, int currentPage, int take, string searchCriteria)
        {
            //get records for given user id and group by process name
            var query = ProcessExplorerDbContext.Processes.Where(i => i.Session.ExplorerUserId == userId);

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                //search processes using given search criteria
                searchCriteria = searchCriteria.ToLower();
                query = query.Where(i => i.ProcessName.ToLower().Contains(searchCriteria));
            }

            //group by process name
            var groupBy = query.GroupBy(i => i.ProcessName);

            //count total number of records
            int count = await groupBy.Select(i => i.Key).CountAsync();

            //get concrete records (PAGINATED)
            var records = await groupBy.Skip((currentPage - 1) * take).Take(take)
                                        .Select(i => new ProcessSearchModel
                                        {
                                            ProcessName = i.Key,
                                            OccuresInNumOfSessions = i.Count(),
                                        })
                                        .ToListAsync();

            return (records, count);
        }

        public async Task<List<ColumnChartStatisticModel>> GetMostUsedProcessesForPeriod(DateTime start, DateTime end, int take)
        {
            //get records for specific period
            return await ProcessExplorerDbContext.Processes.Where(i => i.Detected.Date >= start && i.Detected.Date <= end)
                                        .GroupBy(i => i.ProcessName)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new ColumnChartStatisticModel
                                        {
                                            Label = i.Key,
                                            Value = i.Count()
                                        })
                                        .Take(take)
                                        .ToListAsync();

        }

        public async Task<List<ColumnChartStatisticModel>> GetMostUsedProcessesForUser(Guid userId, int take)
        {
            //get records for specific user
            return await ProcessExplorerDbContext.Processes.Where(i => i.Session.ExplorerUserId == userId)
                                        .GroupBy(i => i.ProcessName)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new ColumnChartStatisticModel
                                        {
                                            Label = i.Key,
                                            Value = i.Count()
                                        })
                                        .Take(take)
                                        .ToListAsync();
        }

        public async Task<List<ColumnChartStatisticModel>> GetProcessesStatsForSessions(Guid userId, int take)
        {
            //get records for specific user
            return await ProcessExplorerDbContext.Sessions.Where(i => i.ExplorerUserId == userId)
                                        .OrderByDescending(i => i.Started)
                                        .Select(i => new ColumnChartStatisticModel
                                        {
                                            Label = $"{i.Started.Day}/{i.Started.Month}/{i.Started.Year} {i.Started.Hour}:{i.Started.Minute}",
                                            Value = i.Processes.Count
                                        })
                                        .Take(take)
                                        .ToListAsync();
        }

        public async Task<TopProcessDayModel> DayWithMostDifferentProcessesForAllTime()
        {
            //get records for specific user
            return await ProcessExplorerDbContext.Processes
                                        .GroupBy(i => i.Detected.Date)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new TopProcessDayModel
                                        {
                                            Day = i.Key,
                                            TotalNumberOfDifferentProcesses = i.Count()
                                        })
                                        .FirstOrDefaultAsync();
        }

        public async Task<TopProcessDayModel> DayWithMostDifferentProcessesForAllTime(Guid userId)
        {
            //get records for specific user
            return await ProcessExplorerDbContext.Processes
                                        .Where(i => i.Session.ExplorerUserId == userId)
                                        .GroupBy(i => i.Detected.Date)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new TopProcessDayModel
                                        {
                                            Day = i.Key,
                                            TotalNumberOfDifferentProcesses = i.Count()
                                        })
                                        .FirstOrDefaultAsync();
        }

        public async Task<List<PieChartStatisticModel>> GetProcessStatsForOSPeriod(DateTime start, DateTime end)
        {
            return await ProcessExplorerDbContext.Processes.Where(i => i.Detected.Date >= start && i.Detected.Date <= end)
                                        .GroupBy(i => i.Session.OS)
                                        .Select(i => new PieChartStatisticModel
                                        {
                                            ChunkName = i.Key,
                                            Quantity = i.Count()
                                        })
                                        .ToListAsync();
        }

        public async Task<List<PieChartStatisticModel>> GetProcessStatsForOSUser(DateTime start, DateTime end, Guid userId)
        {
            return await ProcessExplorerDbContext.Processes.Where(i => i.Detected.Date >= start && i.Detected.Date <= end && i.Session.ExplorerUserId == userId)
                                        .GroupBy(i => i.Session.OS)
                                        .Select(i => new PieChartStatisticModel
                                        {
                                            ChunkName = i.Key,
                                            Quantity = i.Count()
                                        })
                                        .ToListAsync();
        }

        public async Task<(List<ProcessSearchModel>, int)> GetProcessesForUserSession(Guid userId, int currentPage, int take, string searchCriteria, Guid sessionId)
        {
            //get records for specific period and group them by name
            var query = ProcessExplorerDbContext.Processes.Where(i => i.SessionId == sessionId && i.Session.ExplorerUserId == userId);

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                //search processes using given search criteria
                searchCriteria = searchCriteria.ToLower();
                query = query.Where(i => i.ProcessName.ToLower().Contains(searchCriteria));
            }

            //count total number of records
            int count = await query.CountAsync();

            //get concrete records (PAGINATED)
            var records = await query.Skip((currentPage - 1) * take).Take(take)
                                        .Select(i => new ProcessSearchModel { ProcessName = i.ProcessName })
                                        .ToListAsync();

            return (records, count);
        }
    }
}
