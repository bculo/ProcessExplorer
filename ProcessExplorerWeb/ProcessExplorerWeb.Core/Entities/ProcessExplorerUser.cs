using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Entities
{
    public class ProcessExplorerUser : Entity<Guid>, IProcessExplorerUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<ProcessExplorerUserSession> Sessions { get; set; }

        public ProcessExplorerUser(Guid id, string email, string userName)
        {
            Id = id;
            Email = email;
            UserName = userName;
        }
    }
}
