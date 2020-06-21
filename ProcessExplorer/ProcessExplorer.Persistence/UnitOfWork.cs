using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProcessExplorerDbContext _context;

        public ISessionRepository Sessions { get; set; }
        public IAuthenticationRepository Authentication { get; set; }
        public IProcessRepository Process { get; set; }
        public IApplicationRepository Application { get; set; }

        public UnitOfWork(ProcessExplorerDbContext context)
        {
            _context = context;
            Sessions = new SessionRepository(_context);
            Authentication = new AuthenticationRepository(_context);
            Process = new ProcessRepository(_context);
            Application = new ApplicationRepository(_context);
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
