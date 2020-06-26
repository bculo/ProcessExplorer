using MediatR;
using ProcessExplorerWeb.Application.Dtos.Shared;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncApplication
{
    public class SyncApplicationCommand : IRequest
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public DateTime Started { get; set; }
        public List<ApplicationInstanceDto> Applications { get; set; }
    }
}
