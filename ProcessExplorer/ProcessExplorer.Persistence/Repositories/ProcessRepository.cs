using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence.Repositories
{
    public class ProcessRepository : Repository<ProcessEntity>, IProcessRepository
    {
        public ProcessRepository(DbContext context) : base(context)
        {
        }
    }
}
