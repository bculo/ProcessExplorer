using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ISessionRepository Sessions { get; }
        public IProcessRepository Process { get; }
        public IApplicationRepository Application { get; }
        public IAuthenticationRepository Authentication { get; }
        int Commit();
        Task<int> CommitAsync();
    }
}
