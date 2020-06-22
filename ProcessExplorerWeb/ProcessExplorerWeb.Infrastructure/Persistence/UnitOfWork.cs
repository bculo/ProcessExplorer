using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Infrastructure.Persistence.Repos;
using System;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProcessExplorerDbContext _context;

        public IProcessExplorerUserRepository User { get; set; }
        public IProcessExplorerUserSessionRepository Session { get; set; }
        public IApplicationEntityRepository Applications { get; set; }
        public IProcessEntityRepository Process { get; set; }

        public UnitOfWork(ProcessExplorerDbContext context)
        {
            _context = context;
            User = new ProcessExplorerUserRepository(_context);
            Session = new ProcessExplorerUserSessionRepository(_context);
            Applications = new ApplicationEntityRepository(_context);
            Process = new ProcessEntityRepository(_context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
