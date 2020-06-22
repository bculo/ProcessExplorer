using System;
using System.Collections.Generic;

namespace ProcessExplorer.Core.Entities
{
    /// <summary>
    /// Represent user session 
    /// </summary>
    public class Session : Entity<Guid>
    {
        public string UserName { get; set; }
        public DateTime Started { get; set; }
        public string OS { get; set; }
        public DateTime? Finished { get; set; }
        public virtual ICollection<ProcessEntity> ProcessEntities { get; set; }
        public virtual ICollection<ApplicationEntity> Applications { get; set; }
    }
}
