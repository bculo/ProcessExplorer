using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Options
{
    public class IdentityPasswordOptions
    {
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireUppercase { get; set; }
        public int RequiredLength { get; set; }
        public int RequiredUniqueChars { get; set; }
    }
}
