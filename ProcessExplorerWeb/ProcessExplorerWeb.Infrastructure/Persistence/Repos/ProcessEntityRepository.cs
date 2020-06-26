using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
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
            //turn searchCriteria in null if empty
            //if not null keep search criteria value and make it lowercase
            searchCriteria = string.IsNullOrEmpty(searchCriteria) ? null : searchCriteria.ToLower();

            //get records for specific period and group them by name
            //search processes using given search criteria
            var query = ProcessExplorerDbContext.Processes.Where(i => i.Detected.Date >= start.Date && i.Detected.Date <= end.Date
                                            && (searchCriteria != null && i.ProcessName.ToLower().Contains(searchCriteria)))
                                        .GroupBy(i => i.ProcessName);

            //count total number of records
            int count = await query.CountAsync();

            //get concrete records (PAGINATED)
            var records = await query.Skip((currentPage - 1) * take).Take(take)
                                        .Select(i => new ProcessSearchModel
                                        {
                                            ProcessName = i.Key,
                                            OccuresInNumOfSessions = i.Count()
                                        })
                                        .ToListAsync();

            return (records, count);
        }

        public async Task<(List<ProcessSearchModel>, int)> GetProcessesForUser(Guid userId, int currentPage, int take, string searchCriteria)
        {
            //turn searchCriteria in null if empty
            //if not null keep search criteria value and make it lowercase
            searchCriteria = string.IsNullOrEmpty(searchCriteria) ? null : searchCriteria.ToLower();

            //get records for given user id and group by process name
            //search processes using given search criteria
            var query = ProcessExplorerDbContext.Processes.Where(i => i.Session.ProcessExplorerUser.Id == userId 
                                            && (searchCriteria != null && i.ProcessName.ToLower().Contains(searchCriteria)))
                                        .GroupBy(i => i.ProcessName);

            //count total number of records
            int count = await query.CountAsync();

            //get concrete records (PAGINATED)
            var records = await query.Skip((currentPage - 1) * take).Take(take)
                                        .Select(i => new ProcessSearchModel
                                        {
                                            ProcessName = i.Key,
                                            OccuresInNumOfSessions = i.Count()
                                        })
                                        .ToListAsync();

            return (records, count);
        }
    }
}
