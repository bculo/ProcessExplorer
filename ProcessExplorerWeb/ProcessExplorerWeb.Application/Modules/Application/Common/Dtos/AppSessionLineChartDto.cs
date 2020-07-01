using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Common.Dtos
{
    public class AppSessionLineChartDto
    {
        /// <summary>
        /// X -> date when session started
        /// </summary>
        public IEnumerable<DateTime> Date { get; set; }

        /// <summary>
        /// Y -> Number of opened app in session
        /// </summary>
        public IEnumerable<int> Number { get; set; }
    }
}
