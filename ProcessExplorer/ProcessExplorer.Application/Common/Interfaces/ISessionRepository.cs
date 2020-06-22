using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<Session> GetCurrentSession(DateTime logonTime);
        IEnumerable<Session> GetAllWithIncludes();
        Task<List<Session>> GetAllWithIncludesAsync();
        Task<List<Session>> GetAllWithIncludesWihoutCurrentAsync(Guid sessionId);
    }
}
