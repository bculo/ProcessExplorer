using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Options
{
    public class AuthenticationOptions
    {
        public JwtTokenOptions JwtTokenOptions { get; set; }
        public IdentityPasswordOptions IdentityPasswordOptions { get; set; }
        public IdentityUserOptions IdentityUserOptions { get; set; }
        public string DefaultRole { get; set; }
    }
}
