using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Repos
{
    public class ProcessExplorerUserSessionRepository : Repository<ProcessExplorerUserSession>, IProcessExplorerUserSessionRepository
    {
        private ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public ProcessExplorerUserSessionRepository(DbContext context) : base(context)
        {
        }
    }
}
