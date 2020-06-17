using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Persistence.Repositories
{
    public class AuthenticationRepository : Repository<Authentication>, IAuthenticationRepository
    {
        protected ProcessExplorerDbContext ProcessExplorerDbContext => _context as ProcessExplorerDbContext;

        public AuthenticationRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get last token
        /// </summary>
        /// <returns></returns>
        public async Task<Authentication> GetLastToken()
        {
            return await ProcessExplorerDbContext.Authentications.LastAsync();
        }
    }
}
