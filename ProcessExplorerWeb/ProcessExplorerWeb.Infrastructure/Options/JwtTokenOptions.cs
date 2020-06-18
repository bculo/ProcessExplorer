using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Options
{
    public class JwtTokenOptions
    {
        public string Secret { get; set; }
        public int Duration { get; set; }
    }
}
