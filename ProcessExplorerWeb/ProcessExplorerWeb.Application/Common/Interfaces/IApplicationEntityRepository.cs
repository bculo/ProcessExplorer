using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IApplicationEntityRepository : IRepository<ApplicationEntity>
    {
        Task<List<ApplicationEntity>> GetApplicationsForSession(Guid sessionId);
        void BulkAdd(IList<ApplicationEntity> applicationEntities);
    }
}
