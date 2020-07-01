using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Applications
{
    public class ApplicationSearchDetail
    {
        public string Name { get; set; }
        public DateTime Opened { get; set; }
        public DateTime Closed { get; set; }
    }
}
