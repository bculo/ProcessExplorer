using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IProcessEntityRepository : IRepository<ProcessEntity>
    {
        Task<List<ProcessEntity>> GetProcessesForSession(Guid sessionId);
    }
}
