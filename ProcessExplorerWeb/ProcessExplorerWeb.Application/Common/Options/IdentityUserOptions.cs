using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Options
{
    public class IdentityUserOptions
    {
        public bool RequireUniqueEmail { get; set; }
        public int MinimumUsernameLength { get; set; }
        public bool RequireConfirmedEmail { get; set; }
    }
}
