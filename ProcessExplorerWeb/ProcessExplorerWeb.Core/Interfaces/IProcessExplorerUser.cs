using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Interfaces
{
    public interface IProcessExplorerUser
    {
        public Guid Id { get; }
        public string UserName { get; }
        public string Email { get; }
    }
}
