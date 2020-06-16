using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
