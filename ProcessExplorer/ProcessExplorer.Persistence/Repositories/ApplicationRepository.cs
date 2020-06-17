using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
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

        public async Task BulkAddAsync(IList<ApplicationEntity> applicationEntities)
        {
            await ProcessExplorerDbContext.BulkInsertAsync(applicationEntities);
        }

        public async Task BulkRemoveAsync(IList<ApplicationEntity> applicationEntities)
        {
            await ProcessExplorerDbContext.BulkDeleteAsync(applicationEntities);
        }
    }
}
