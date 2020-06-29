using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Dtos
{
    public class ColumnChartDto
    {
        public IEnumerable<string> Label { get; set; }
        public IEnumerable<int> Value { get; set; }
    }
}
