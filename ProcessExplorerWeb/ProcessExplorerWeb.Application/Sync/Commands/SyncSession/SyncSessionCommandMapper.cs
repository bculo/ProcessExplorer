using Mapster;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    /// <summary>
    /// Mapster settings for Sync command
    /// </summary>
    public class SyncSessionCommandMapper : IRegister
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
        }
    }
}
