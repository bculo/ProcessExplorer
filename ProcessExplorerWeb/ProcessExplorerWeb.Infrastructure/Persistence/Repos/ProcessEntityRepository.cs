using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
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
    }
}
