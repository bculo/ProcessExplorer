using Mapster;
using ProcessExplorer.Api.Soap.Models;
using ProcessExplorerWeb.Application.Sync.Commands.SyncApplication;
using ProcessExplorerWeb.Application.Sync.Commands.SyncProcess;
using ProcessExplorerWeb.Application.Sync.Commands.SyncSession;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Soap
{
    public class SoapMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SyncSessionModel, SyncSessionCommand> ()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.SessionId, src => Guid.Parse(src.SessionId))
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Applications, src => src.Applications)
                .Map(dst => dst.Processes, src => src.Processes);

            config.ForType<SyncProcessModel, SyncProcessCommand>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.SessionId, src => Guid.Parse(src.SessionId))
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Processes, src => src.Processes);

            config.ForType<ProcessInstanceModel, ProcessInstanceDto>()
                .Map(dst => dst.Name, src => src.Name)
                .Map(dst => dst.SessionId, src => Guid.Parse(src.SessionId))
                .Map(dst => dst.Detected, src => src.Detected);

            config.ForType<ApplicationInstanceModel, ApplicationInstanceDto>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.LastUse, src => src.LastUse)
                .Map(dst => dst.Name, src => src.Name)
                .Map(dst => dst.SessionId, src => Guid.Parse(src.SessionId));

            config.ForType<SyncApplicationModel, SyncApplicationCommand>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.SessionId, src => Guid.Parse(src.SessionId))
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Applications, src => src.Applications);
        }
    }
}
