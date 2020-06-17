using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IAuthenticationRepository : IRepository<Authentication>
    {
        Task<Authentication> GetLastToken();
    }
}
