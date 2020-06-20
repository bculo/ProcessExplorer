using System;

namespace ProcessExplorerWeb.Core.Entities
{
    public class ProcessExplorerUserSession : Entity<Guid>
    {
        public string UserName { get; set; }
        public DateTime Started { get; set; }
        public DateTime Inserted { get; set; }
        public Guid ExplorerUserId { get; set; }
        public virtual ProcessExplorerUser ProcessExplorerUser { get; set; }
    }
}
