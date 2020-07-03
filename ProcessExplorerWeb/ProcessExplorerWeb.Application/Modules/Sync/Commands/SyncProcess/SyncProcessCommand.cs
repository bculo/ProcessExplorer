using MediatR;
using ProcessExplorerWeb.Application.Modules.Sync.Common.Dtos;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncProcess
{
    public class SyncProcessCommand : SyncCommand, IRequest
    {
        public List<ProcessInstanceDto> Processes { get; set; }
    }
}
