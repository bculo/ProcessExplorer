using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IProcessRepository : IRepository<ProcessEntity>
    {
        Task<List<ProcessEntity>> GetEntitesForSession(Guid sessionId);
        void BulkAdd(IList<ProcessEntity> processEntities);
    }
}
