using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Identity
{
    public class IdentityAppRole : IdentityRole<Guid>
    {
        public virtual ICollection<IdentityAppUserRole> UserRoles { get; set; }
    }
}
