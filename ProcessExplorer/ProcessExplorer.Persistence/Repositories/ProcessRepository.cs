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
    public class ProcessRepository : Repository<ProcessEntity>, IProcessRepository
    {
        protected ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public ProcessRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ProcessEntity>> GetEntitesForSession(Guid sessionId)
        {
            return await ProcessExplorerDbContext.ProcessEntities.Where(i => i.SessionId == sessionId).ToListAsync();
        }

        public void BulkAdd(IList<ProcessEntity> processEntities)
        {
            _context.BulkInsert(processEntities);
        }
    }
}
