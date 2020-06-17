using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Core.Entities
{
    public class Authentication : Entity<int>
    {
        public string Content { get; set; }
        public DateTime Inserted { get; set; }
    }
}
