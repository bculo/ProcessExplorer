using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Queries.Applications
{
    public class TopApplicationDay : IQueryResult
    {
        public DateTime Day { get; set; }
        public int Number { get; set; }
    }
}
