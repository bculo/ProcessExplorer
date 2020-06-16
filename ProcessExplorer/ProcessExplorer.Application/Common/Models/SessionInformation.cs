using System;

namespace ProcessExplorer.Application.Common.Models
{
    public class SessionInformation
    {
        public Guid SessionId { get; set; }
        public DateTime SessionStarted { get; set; }
    }
}
