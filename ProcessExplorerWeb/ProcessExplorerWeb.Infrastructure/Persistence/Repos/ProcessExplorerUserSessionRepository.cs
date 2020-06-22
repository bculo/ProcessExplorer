using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Repos
{
    public class ProcessExplorerUserSessionRepository : Repository<ProcessExplorerUserSession>, IProcessExplorerUserSessionRepository
    {
        private ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public ProcessExplorerUserSessionRepository(DbContext context) : base(context)
        {
        }

        public async Task<ProcessExplorerUserSession> GetSessionWithWithAppsAndProcesses(Guid sessionId, Guid userId)
        {
            return await ProcessExplorerDbContext.Sessions
                            .Include(i => i.Applications)
                            .Include(i => i.Processes)
                            .SingleOrDefaultAsync(i => i.Id == sessionId && i.ExplorerUserId == userId);
        }
    }
}
