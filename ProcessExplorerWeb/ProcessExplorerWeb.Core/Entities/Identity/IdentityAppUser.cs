using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Identity
{
    public class IdentityAppUser : IdentityUser<Guid>
    {
        public virtual ICollection<IdentityAppUserRole> UserRoles { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public string ProfilePicture { get; set; }

        public IdentityAppUser()
        {
            if(UserRoles == null)
                UserRoles = new HashSet<IdentityAppUserRole>();

            if(Claims == null)
                Claims = new HashSet<IdentityUserClaim<Guid>>();
        }
    }
}
