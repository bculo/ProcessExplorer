using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Queries.Charts;
using ProcessExplorerWeb.Core.Queries.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Repos
{
    public class ProcessExplorerUserSessionRepository : Repository<ProcessExplorerUserSession>, IProcessExplorerUserSessionRepository
    {
        private ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public ProcessExplorerUserSessionRepository(DbContext context) : base(context)
        {
        }

        public async Task<ProcessExplorerUserSession> GetSessionWithAppsAndProcesses(Guid sessionId, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions
                            .Include(i => i.Applications)
                            .Include(i => i.Processes)
                            .SingleOrDefaultAsync(i => i.ComputerSessionId == sessionId && i.ExplorerUserId == userId);
        }

        public async Task<(List<SessionDetail>, int)> FilterSessions(Guid userId, DateTime? selectedDate, int page, int take)
        {
            //all session for userId
            var query = ProcessExplorerDbContext.Sessions.Where(i => i.ExplorerUserId == userId);

            //set date in query if selectedDate != null
            if (selectedDate.HasValue)
                query = query.Where(i => i.Started.Date == selectedDate.Value.Date);

            //count records
            int recordsCount = await query.CountAsync();

            //get paginated result
            var records = await query.OrderByDescending(i => i.Started.Date)
                            .Skip((page - 1) * take)
                            .Take(take)
                            .Select(i => new SessionDetail
                            {
                                SessionId = i.Id,
                                DifferentProcesses = i.Processes.Count,
                                ApplicationNum = i.Applications.Count,
                                Started = i.Started,
                                OS = i.OS,
                                UserName = i.UserName
                            })
                            .ToListAsync();

            return (records, recordsCount);
        }

        public async Task<ProcessExplorerUserSession> GetSelectedSession(Guid userId, Guid sessionId)
        {
            return await ProcessExplorerDbContext.Sessions.SingleOrDefaultAsync(i => i.ExplorerUserId == userId && i.Id == sessionId);
        }

        public async Task<List<PieChartItem>> GetOperatingSystemStatistics(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started.Date >= startOfPeriod && i.Started.Date <= endOfPeriod)
                            .GroupBy(i => i.OS) //Group by operating system name
                            .Select(i => new PieChartItem
                            {
                                Quantity = i.Count(),
                                ChunkName = i.Key
                            })
                            .ToListAsync();
        }

        public async Task<TopSessionDay> GetMostActiveDay(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started.Date >= startOfPeriod && i.Started.Date <= endOfPeriod)
                            .GroupBy(i => i.Started.Date)
                            .Select(i => new TopSessionDay
                            {
                                Date = i.Key,
                                NumberOfSessions = i.Count()
                            })
                            .OrderByDescending(i => i.NumberOfSessions)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<PieChartItem>> GetOperatingSystemStatisticsForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started.Date >= startOfPeriod && i.Started.Date <= endOfPeriod && i.ExplorerUserId == userId)
                            .GroupBy(i => i.OS) //Group by operating system name
                            .Select(i => new PieChartItem
                            {
                                Quantity = i.Count(),
                                ChunkName = i.Key
                            })
                            .ToListAsync();
        }

        public async Task<TopSessionDay> GetMostActiveDayForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started.Date >= startOfPeriod && i.Started.Date <= endOfPeriod && i.ExplorerUserId == userId)
                            .GroupBy(i => i.Started.Date)
                            .Select(i => new TopSessionDay
                            {
                                Date = i.Key,
                                NumberOfSessions = i.Count()
                            })
                            .OrderByDescending(i => i.NumberOfSessions)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<SessionDateChartItem>> GetActivityChartStatistics(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started.Date >= startOfPeriod && i.Started.Date <= endOfPeriod)
                            .GroupBy(i => i.Started.Date)
                            .Select(i => new SessionDateChartItem
                            {
                                Date = i.Key,
                                Number = i.Count()
                            })
                            .OrderBy(i => i.Date)
                            .ToListAsync();
        }

        public async Task<List<SessionDateChartItem>> GetActivityChartStatisticsForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started.Date >= startOfPeriod && i.Started.Date <= endOfPeriod && i.ExplorerUserId == userId)
                            .GroupBy(i => i.Started.Date)
                            .Select(i => new SessionDateChartItem
                            {
                                Date = i.Key,
                                Number = i.Count()
                            })
                            .OrderBy(i => i.Date)
                            .ToListAsync();
        }

        public async Task<ProcessExplorerUserSession> GetSessionWithProcesses(Guid sessionId, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions
                            .Include(i => i.Processes)
                            .SingleOrDefaultAsync(i => i.ComputerSessionId == sessionId && i.ExplorerUserId == userId);
        }

        public async Task<ProcessExplorerUserSession> GetSessionWithApps(Guid sessionId, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions
                            .Include(i => i.Applications)
                            .SingleOrDefaultAsync(i => i.ComputerSessionId == sessionId && i.ExplorerUserId == userId);
        }

        public async Task<int> GetNumberOfSessinsForPeriod(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started.Date >= startOfPeriod && i.Started.Date <= endOfPeriod)
                            .CountAsync();
        }

        public async Task<int> GetNumberOfSessinsForUser(Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.ExplorerUserId == userId)
                            .CountAsync();
        }
    }
}
