﻿using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Persistence.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        protected ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public SessionRepository(ProcessExplorerDbContext context) : base(context)
        {
        }

        public async Task<Session> GetCurrentSession(DateTime logonTime)
        {
            return await ProcessExplorerDbContext.Sessions.SingleOrDefaultAsync(i => i.Started == logonTime);
        }

        public IEnumerable<Session> GetAllWithIncludes()
        {
            return ProcessExplorerDbContext.Sessions.Include(i => i.Applications)
                            .Include(i => i.ProcessEntities)
                            .AsEnumerable();
        }

        public async Task<List<Session>> GetAllWithIncludesAsync()
        {
            return  await ProcessExplorerDbContext.Sessions.Include(i => i.Applications)
                            .Include(i => i.ProcessEntities)
                            .ToListAsync();
        }

        public async Task<List<Session>> GetAllWithIncludesWihoutCurrentAsync(Guid sessionId)
        {
            return await ProcessExplorerDbContext.Sessions.Where(i => i.Id != sessionId)
                            .Include(i => i.Applications)
                            .Include(i => i.ProcessEntities)
                            .ToListAsync();
        }
    }
}
