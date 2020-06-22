using MediatR;
using System;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    public class SyncSessionCommand : IRequest
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public DateTime Started { get; set; }
        public List<SynSessionApplicationInfoCommand> Applications { get; set; }
        public List<SynSessionProcessInfoCommand> Processes { get; set; }
    }

    public class SynSessionApplicationInfoCommand
    {
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime Saved { get; set; }
    }

    public class SynSessionProcessInfoCommand
    {
        public string Name { get; set; }
        public DateTime Saved { get; set; }
    }
}
