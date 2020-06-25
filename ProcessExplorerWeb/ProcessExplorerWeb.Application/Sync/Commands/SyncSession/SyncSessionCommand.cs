using MediatR;
using ProcessExplorerWeb.Application.Dtos.Models;
using ProcessExplorerWeb.Application.Dtos.Shared;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    /// <summary>
    /// Command for synchronization
    /// </summary>
    public class SyncSessionCommand : IRequest
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public DateTime Started { get; set; }
        public List<SyncSessionApplicationInfoCommand> Applications { get; set; }
        public List<SyncSessionProcessInfoCommand> Processes { get; set; }
    }

    public class SyncSessionApplicationInfoCommand : ApplicationExplorerDto 
    {
        public Guid SessionId { get; set; }
    }

    public class SyncSessionProcessInfoCommand : ProcessExplorerDto 
    {
        public Guid SessionId { get; set; }
    }
}
