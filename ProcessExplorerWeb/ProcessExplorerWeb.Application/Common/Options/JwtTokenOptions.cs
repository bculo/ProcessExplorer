using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Options
{
    public class JwtTokenOptions
    {
        public string Secret { get; set; }
        public int Duration { get; set; }
    }
}
