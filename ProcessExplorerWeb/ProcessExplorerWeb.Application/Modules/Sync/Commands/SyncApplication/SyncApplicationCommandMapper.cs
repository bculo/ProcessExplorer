﻿using Mapster;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncApplication
{
    public class SyncApplicationCommandMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SyncApplicationCommand, ProcessExplorerUserSession>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.ComputerSessionId, src => src.SessionId)
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Applications, src => src.Applications)
                .Ignore(dst => dst.Processes);
        }
    }
}
