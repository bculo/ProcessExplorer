using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
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

        public void BulkAdd(IList<ApplicationEntity> applicationEntities)
        {
            _context.BulkInsert(applicationEntities);
        }
    }
}
