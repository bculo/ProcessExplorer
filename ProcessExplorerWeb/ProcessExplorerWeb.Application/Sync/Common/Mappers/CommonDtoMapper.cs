using Mapster;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Application.Sync.Common.Mappers
{
    public class CommonDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ProcessInstanceDto, ProcessEntity>()
                .Map(dst => dst.ProcessName, src => src.Name)
                .Map(dst => dst.Detected, src => src.Detected)
                .Map(dst => dst.SessionId, src => src.SessionId)
                .Ignore(dst => dst.Session);

            config.ForType<ApplicationInstanceDto, ApplicationEntity>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.Name, src => src.Name)
                .Map(dst => dst.Closed, src => src.LastUse)
                .Map(dst => dst.SessionId, src => src.SessionId)
                .Ignore(dst => dst.Session);
        }
    }
}
