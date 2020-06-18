using Microsoft.AspNetCore.Identity;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Identity
{
    /// <summary>
    /// For authentication prupose only
    /// </summary>
    public class IdentityAppUser : IdentityUser<Guid>, IProcessExplorerUser
    {
        public virtual ICollection<IdentityAppUserRole> UserRoles { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

        public IdentityAppUser()
        {
            if(UserRoles == null)
                UserRoles = new HashSet<IdentityAppUserRole>();

            if(Claims == null)
                Claims = new HashSet<IdentityUserClaim<Guid>>();
        }
    }
}
