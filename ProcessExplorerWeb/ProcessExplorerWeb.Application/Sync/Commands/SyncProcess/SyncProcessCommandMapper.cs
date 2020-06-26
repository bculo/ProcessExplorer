using Mapster;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncProcess
{
    public class SyncProcessCommandMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SyncProcessCommand, ProcessExplorerUserSession>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.Id, src => src.SessionId)
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Processes, src => src.Processes)
                .Ignore(dst => dst.Applications);
        }
    }
}
