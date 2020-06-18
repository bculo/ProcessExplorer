using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Identity
{
    public class IdentityAppUserRole : IdentityUserRole<Guid>
    {
        public virtual IdentityAppUser User { get; set; }
        public virtual IdentityAppRole Role { get; set; }
    }
}
