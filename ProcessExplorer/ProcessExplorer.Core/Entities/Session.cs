using System;

namespace ProcessExplorer.Core.Entities
{
    /// <summary>
    /// Represent user session 
    /// </summary>
    public class Session : Entity<Guid>
    {
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
    }
}
