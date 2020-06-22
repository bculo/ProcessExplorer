using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Core.Entities
{
    public class ProcessEntity : Entity<int>
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public DateTime Saved { get; set; }
        public Guid SessionId { get; set; }
        public virtual Session Session { get; set; }
    }
}
