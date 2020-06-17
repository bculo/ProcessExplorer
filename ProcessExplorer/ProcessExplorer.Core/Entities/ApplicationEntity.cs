using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Core.Entities
{
    public class ApplicationEntity : Entity<int>
    {
        public string ApplicationName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Saved { get; set; }
        public Guid SessionId { get; set; }
        public virtual Session Session { get; set; }
    }
}
