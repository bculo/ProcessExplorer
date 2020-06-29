using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Dtos
{
    public class PieChartDto
    {
        public IEnumerable<string> Name { get; set; }
        public IEnumerable<int> Quantity { get; set; }
    }
}
