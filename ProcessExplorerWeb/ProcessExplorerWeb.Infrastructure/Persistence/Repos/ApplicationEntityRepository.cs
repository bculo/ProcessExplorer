using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Queries.Applications;
using ProcessExplorerWeb.Core.Queries.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Repos
{
    public class ApplicationEntityRepository : Repository<ApplicationEntity>, IApplicationEntityRepository
    {
        private ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public ApplicationEntityRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ApplicationEntity>> GetApplicationsForSession(Guid sessionId)
        {
            return await ProcessExplorerDbContext.Applications.Where(i => i.SessionId == sessionId)
                                        .ToListAsync();
        }

        public async Task<TopApplicationDay> DayWithMostOpenedAppsAllTime()
        {
            return await ProcessExplorerDbContext.Applications
                                        .GroupBy(i => i.Started.Date)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new TopApplicationDay
                                        {
                                            Day = i.Key,
                                            Number = i.Count()
                                        })
                                        .FirstOrDefaultAsync();
        }

        public async Task<TopApplicationDay> DayWithMostOpenedAppsAllTime(Guid userId)
        {
            return await ProcessExplorerDbContext.Applications
                                        .Where(i => i.Session.ExplorerUserId == userId)
                                        .GroupBy(i => i.Started.Date)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new TopApplicationDay
                                        {
                                            Day = i.Key,
                                            Number = i.Count()
                                        })
                                        .FirstOrDefaultAsync();
        }

        public async Task<List<PieChartItem>> GetApplicationStatsForOSPeriod(DateTime start, DateTime end)
        {
            return await ProcessExplorerDbContext.Applications
                                        .Where(i => i.Started.Date >= start && i.Started.Date <= end)
                                        .GroupBy(i => i.Session.OS)
                                        .Select(i => new PieChartItem
                                        {
                                            ChunkName = i.Key,
                                            Quantity = i.Count()
                                        })
                                        .ToListAsync();
        }

        public async Task<List<PieChartItem>> GetApplicationStatsForOSUser(DateTime start, DateTime end, Guid userId)
        {
            return await ProcessExplorerDbContext.Applications
                                        .Where(i => i.Started.Date >= start && i.Started.Date <= end && i.Session.ExplorerUserId == userId)
                                        .GroupBy(i => i.Session.OS)
                                        .Select(i => new PieChartItem
                                        {
                                            ChunkName = i.Key,
                                            Quantity = i.Count()
                                        })
                                        .ToListAsync();
        }

        public async Task<(List<ApplicationSearchItem>, int)> GetAppsForPeriod(DateTime start, DateTime end, int currentPage, int take, string searchCriteria)
        {
            //get records for specific period and group them by name
            var query = ProcessExplorerDbContext.Applications.Where(i => i.Started.Date >= start && i.Started.Date <= end);

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                //search applications using given search criteria
                searchCriteria = searchCriteria.ToLower();
                query = query.Where(i => i.Name.ToLower().Contains(searchCriteria));
            }

            //group by process name
            var groupBy = query.GroupBy(i => i.Name);

            //count total number of records
            int count = await groupBy.Select(i => i.Key).CountAsync();

            //get concrete records (PAGINATED)
            var records = await groupBy.Skip((currentPage - 1) * take).Take(take)
                                        .Select(i => new ApplicationSearchItem
                                        {
                                            ApplicationName = i.Key,
                                            OccuresNumOfTime = i.Count(),
                                        })
                                        .ToListAsync();

            return (records, count);
        }

        public async Task<(List<ApplicationSearchItem>, int)> GetAppsForUser(Guid userId, int currentPage, int take, string searchCriteria)
        {
            //get records for specific period and group them by name
            var query = ProcessExplorerDbContext.Applications.Where(i => i.Session.ExplorerUserId == userId);

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                //search applications using given search criteria
                searchCriteria = searchCriteria.ToLower();
                query = query.Where(i => i.Name.ToLower().Contains(searchCriteria));
            }

            //group by process name
            var groupBy = query.GroupBy(i => i.Name);

            //count total number of records
            int count = await groupBy.Select(i => i.Key).CountAsync();

            //get concrete records (PAGINATED)
            var records = await groupBy.Skip((currentPage - 1) * take).Take(take)
                                        .Select(i => new ApplicationSearchItem
                                        {
                                            ApplicationName = i.Key,
                                            OccuresNumOfTime = i.Count(),
                                        })
                                        .ToListAsync();

            return (records, count);
        }

        public async Task<(List<ApplicationSearchDetail>, int)> GetApplicationsForUserSession(Guid userId, int currentPage, int take, string searchCriteria, Guid sessionId)
        {
            //get records for specific period and group them by name
            var query = ProcessExplorerDbContext.Applications.Where(i => i.SessionId == sessionId && i.Session.ExplorerUserId == userId);

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                //search processes using given search criteria
                searchCriteria = searchCriteria.ToLower();
                query = query.Where(i => i.Name.ToLower().Contains(searchCriteria));
            }

            //count total number of records
            int count = await query.CountAsync();

            //get concrete records (PAGINATED)
            var records = await query.Skip((currentPage - 1) * take)
                                        .Take(take)
                                        .Select(i => new ApplicationSearchDetail
                                        { 
                                            Name = i.Name, 
                                            Closed = i.Started,
                                            Opened = i.Closed,
                                        })
                                        .ToListAsync();

            return (records, count);
        }

        public async Task<List<ColumnChartItem>> GetMostUsedApplicationsForPeriod(DateTime start, DateTime end, int take)
        {
            return await ProcessExplorerDbContext.Applications.Where(i => i.Started.Date >= start && i.Started.Date <= end)
                                        .GroupBy(i => i.Name)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new ColumnChartItem
                                        {
                                            Label = i.Key,
                                            Value = i.Count()
                                        })
                                        .Take(take)
                                        .ToListAsync();
        }

        public async Task<List<ColumnChartItem>> GetMostUsedApplicationsForUser(Guid userId, int take)
        {
            return await ProcessExplorerDbContext.Applications.Where(i => i.Session.ExplorerUserId == userId)
                                        .GroupBy(i => i.Name)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new ColumnChartItem
                                        {
                                            Label = i.Key,
                                            Value = i.Count()
                                        })
                                        .Take(take)
                                        .ToListAsync();
        }

        public async Task<List<ColumnChartItem>> GetMostUsedApplicationsForUserSession(Guid userId, Guid sessionId, int take)
        {
            return await ProcessExplorerDbContext.Applications.Where(i => i.Session.ExplorerUserId == userId && i.SessionId == sessionId)
                                        .GroupBy(i => i.Name)
                                        .OrderByDescending(i => i.Count())
                                        .Select(i => new ColumnChartItem
                                        {
                                            Label = i.Key,
                                            Value = i.Count()
                                        })
                                        .Take(take)
                                        .ToListAsync();
        }

        public async Task<List<AppSessionLineChartItem>> GetNumberOfAppsLastSessions(Guid userId, int take)
        {
            return await ProcessExplorerDbContext.Applications.Where(i => i.Session.ExplorerUserId == userId)
                                        .OrderByDescending(i => i.Session.Started)
                                        .Select(i => new AppSessionLineChartItem
                                        {
                                            Date = i.Session.Started,
                                            Number = i.Session.Applications.Count
                                        })
                                        .Take(take)
                                        .ToListAsync();
        }
    }
}
