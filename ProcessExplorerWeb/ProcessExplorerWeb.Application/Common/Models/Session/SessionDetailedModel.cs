using ProcessExplorerWeb.Core.Entities;
using System;

namespace ProcessExplorerWeb.Application.Common.Models.Session
{
    public class SessionDetailedModel
    {
        public Guid SessionId { get; set; }
        public DateTime Started { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public int DifferentProcesses { get; set; }
        public int ApplicationNum { get; set; }
    }
}
