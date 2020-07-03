using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Sync.Common.Dtos
{
    /// <summary>
    /// Root command for sync commands
    /// </summary>
    public abstract class SyncCommand
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public DateTime Started { get; set; }
        public Guid UserId { get; set; }
    }
}
