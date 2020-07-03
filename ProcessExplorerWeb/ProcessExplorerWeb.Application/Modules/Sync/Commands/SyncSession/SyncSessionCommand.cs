using MediatR;
using ProcessExplorerWeb.Application.Modules.Sync.Common.Dtos;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    /// <summary>
    /// Command for synchronization
    /// </summary>
    public class SyncSessionCommand : SyncCommand, IRequest
    {
        public List<ApplicationInstanceDto> Applications { get; set; }
        public List<ProcessInstanceDto> Processes { get; set; }
    }
}
