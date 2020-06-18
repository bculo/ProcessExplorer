using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Repos
{
    public class ProcessExplorerUserRepository : Repository<ProcessExplorerUser>, IProcessExplorerUserRepository
    {
        public ProcessExplorerUserRepository(DbContext context) : base(context)
        {
        }
    }
}
