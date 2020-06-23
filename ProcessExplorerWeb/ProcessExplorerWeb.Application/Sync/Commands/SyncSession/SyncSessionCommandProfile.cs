using Mapster;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    /// <summary>
    /// Mapster settings for Sync command
    /// </summary>
    public class SyncSessionCommandProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SyncSessionCommand, ProcessExplorerUserSession>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.Id, src => src.SessionId)
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Applications, src => src.Applications)
                .Map(dst => dst.Processes, src => src.Processes);

            config.ForType<SyncSessionProcessInfoCommand, ProcessEntity>()
                .Map(dst => dst.ProcessName, src => src.Name)
                .Map(dst => dst.Detected, src => src.Detected)
                .Map(dst => dst.SessionId, src => src.SessionId)
                .Ignore(dst => dst.Session);

            config.ForType<SyncSessionApplicationInfoCommand, ApplicationEntity>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.Name, src => src.Name)
                .Map(dst => dst.Closed, src => src.LastUse)
                .Map(dst => dst.SessionId, src => src.SessionId)
                .Ignore(dst => dst.Session);
        }
    }
}
