using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Session.SharedDtos
{
    public class SessionLineChartDto
    {
        public IEnumerable<string> Date { get; set; }
        public IEnumerable<int> Number { get; set; }
    }
}
