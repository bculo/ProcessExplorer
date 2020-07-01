using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchSessionsApps
{
    public class SearchSessionsAppsQueryResponseDto
    {
        public string Name { get; set; }
        public DateTime Opened { get; set; }
        public DateTime Closed { get; set; }
    }
}
