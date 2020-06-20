﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProcessExplorerUserRepository User { get; }
        IProcessExplorerUserSessionRepository Session { get; }
        int Commit();
        Task<int> CommitAsync();
    }
}
