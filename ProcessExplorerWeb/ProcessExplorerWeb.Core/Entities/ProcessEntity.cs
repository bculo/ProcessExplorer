using System;

namespace ProcessExplorerWeb.Core.Entities
{
    public class ProcessEntity : Entity<Guid>
    {
        /// <summary>
        /// Name of process
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Process ID
        /// </summary>
        public int? PID { get; set; }

        /// <summary>
        /// Reference on user session
        /// </summary>
        public Guid SessionId { get; set; }
        public virtual ProcessExplorerUserSession Session { get; set; }
    }
}
