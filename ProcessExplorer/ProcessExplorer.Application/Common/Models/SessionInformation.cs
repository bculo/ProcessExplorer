using System;

namespace ProcessExplorer.Application.Common.Models
{
    public class SessionInformation
    {
        public string User { get; set; }
        public Guid SessionId { get; set; }
        public DateTime SessionStarted { get; set; }
        public bool Offline { get; set; }
        public string OS { get; set; }
    }
}
