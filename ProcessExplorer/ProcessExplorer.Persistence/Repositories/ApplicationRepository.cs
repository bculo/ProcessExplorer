using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence.Repositories
{
    public class ApplicationRepository : Repository<ApplicationEntity>, IApplicationRepository
    {
        public ApplicationRepository(DbContext context) : base(context)
        {
        }
    }
}
