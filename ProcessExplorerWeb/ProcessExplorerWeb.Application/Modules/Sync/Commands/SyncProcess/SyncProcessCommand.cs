using MediatR;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncProcess
{
    public class SyncProcessCommand : IRequest
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public DateTime Started { get; set; }
        public List<ProcessInstanceDto> Processes { get; set; }
    }
}
