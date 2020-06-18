using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProcessExplorerUserRepository User { get; }
        int Commit();
        Task<int> CommitAsync();
    }
}
