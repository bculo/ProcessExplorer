using MediatR;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
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
        public List<ApplicationInstanceDto> Applications { get; set; }
        public List<ProcessInstanceDto> Processes { get; set; }
    }
}
