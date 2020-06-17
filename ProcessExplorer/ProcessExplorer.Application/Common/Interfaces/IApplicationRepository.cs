using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IApplicationRepository : IRepository<ApplicationEntity>
    {
        void BulkAdd(IList<ApplicationEntity> applicationEntities);
        void BulkRemove(IList<ApplicationEntity> applicationEntities);
        Task BulkAddAsync(IList<ApplicationEntity> applicationEntities);
        Task BulkRemoveAsync(IList<ApplicationEntity> applicationEntities);
    }
}
