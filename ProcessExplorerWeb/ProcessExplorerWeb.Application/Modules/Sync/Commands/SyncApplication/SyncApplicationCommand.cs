using MediatR;
using ProcessExplorerWeb.Application.Modules.Sync.Common.Dtos;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncApplication
{
    public class SyncApplicationCommand : SyncCommand, IRequest
    {
        public List<ApplicationInstanceDto> Applications { get; set; }
    }
}
