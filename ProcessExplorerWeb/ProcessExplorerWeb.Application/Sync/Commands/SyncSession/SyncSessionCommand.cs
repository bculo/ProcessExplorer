using MediatR;
using ProcessExplorerWeb.Application.Dtos.Models;
using ProcessExplorerWeb.Application.Dtos.Shared;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    public class SyncSessionCommand : IRequest
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public DateTime Started { get; set; }
        //public Guid UserId { get; set; }
        public List<SynSessionApplicationInfoCommand> Applications { get; set; }
        public List<SynSessionProcessInfoCommand> Processes { get; set; }
    }

    public class SynSessionApplicationInfoCommand : ApplicationExplorerModel { }

    public class SynSessionProcessInfoCommand : ProcessExplorerModel { }
}
