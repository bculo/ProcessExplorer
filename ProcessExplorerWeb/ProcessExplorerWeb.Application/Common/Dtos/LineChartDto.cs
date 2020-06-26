using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Dtos
{
    public class LineChartDto
    {
        /// <summary>
        /// X point
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Y point
        /// </summary>
        public int Value { get; set; }
    }
}
