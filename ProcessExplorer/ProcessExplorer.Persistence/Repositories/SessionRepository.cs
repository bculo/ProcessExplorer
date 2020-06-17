using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        protected ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public SessionRepository(ProcessExplorerDbContext context) : base(context)
        {
        }
    }
}
