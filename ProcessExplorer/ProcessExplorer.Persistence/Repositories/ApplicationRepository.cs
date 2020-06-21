using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Persistence.Repositories
{
    public class ApplicationRepository : Repository<ApplicationEntity>, IApplicationRepository
    {
        protected ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public ApplicationRepository(DbContext context) : base(context)
        {
        }

        public void BulkAdd(IList<ApplicationEntity> applicationEntities)
        {
            ProcessExplorerDbContext.BulkInsert(applicationEntities);
        }

        public void BulkRemove(IList<ApplicationEntity> applicationEntities)
        {
            ProcessExplorerDbContext.BulkDelete(applicationEntities);
        }

        public void BulkUpdate(IList<ApplicationEntity> applicationEntities)
        {
            ProcessExplorerDbContext.BulkUpdate(applicationEntities);
        }

        public async Task<List<ApplicationEntity>> GetEntitesForSession(Guid sessionId)
        {
            return await ProcessExplorerDbContext.ApplicationEntities
                            .Where(i => i.SessionId == sessionId)
                            .ToListAsync();
        }
    }
}
