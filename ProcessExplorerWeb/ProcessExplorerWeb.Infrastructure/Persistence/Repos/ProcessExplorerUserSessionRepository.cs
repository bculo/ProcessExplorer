using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Charts.Shared;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Models.Session;
using ProcessExplorerWeb.Core.Entities;
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
                            .SingleOrDefaultAsync(i => i.Id == sessionId && i.ExplorerUserId == userId);
        }

        public async Task<(List<SessionDetailedModel>, int)> FilterSessions(Guid userId, DateTime? selectedDate, int page, int take)
        {
            //all session for userId
            var query = ProcessExplorerDbContext.Sessions.Where(i => i.ExplorerUserId == userId);

            //set date in query if selectedDate != null
            if (selectedDate.HasValue)
                query = query.Where(i => i.Started.Date == selectedDate.Value.Date);

            //count records
            int recordsCount = await query.CountAsync();

            //get paginated result
            var records = await query.OrderByDescending(i => i.Started)
                            .Skip((page - 1) * take).Take(take)
                            .Select(i => new SessionDetailedModel
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

        public async Task<List<PieChartStatisticModel>> OperatingSystemStatistics(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started >= startOfPeriod && i.Started <= endOfPeriod)
                            .GroupBy(i => i.OS) //Group by operating system name
                            .Select(i => new PieChartStatisticModel
                            {
                                Quantity = i.Count(),
                                ChunkName = i.Key
                            })
                            .ToListAsync();
        }

        public async Task<PopularSessionDayModel> GetMostActiveDay(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started >= startOfPeriod && i.Started <= endOfPeriod)
                            .GroupBy(i => i.Started.Date)
                            .Select(i => new PopularSessionDayModel
                            {
                                Date = i.Key,
                                NumberOfSessions = i.Count()
                            })
                            .OrderByDescending(i => i.NumberOfSessions)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<PieChartStatisticModel>> OperatingSystemStatisticsForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started >= startOfPeriod && i.Started <= endOfPeriod && i.ExplorerUserId == userId)
                            .GroupBy(i => i.OS) //Group by operating system name
                            .Select(i => new PieChartStatisticModel
                            {
                                Quantity = i.Count(),
                                ChunkName = i.Key
                            })
                            .ToListAsync();
        }

        public async Task<PopularSessionDayModel> GetMostActiveDayForUser(DateTime startOfPeriod, DateTime endOfPeriod, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Started >= startOfPeriod && i.Started <= endOfPeriod && i.ExplorerUserId == userId)
                            .GroupBy(i => i.Started.Date)
                            .Select(i => new PopularSessionDayModel
                            {
                                Date = i.Key,
                                NumberOfSessions = i.Count()
                            })
                            .OrderByDescending(i => i.NumberOfSessions)
                            .FirstOrDefaultAsync();
        }
    }
}
