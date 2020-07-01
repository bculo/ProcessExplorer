using Mapster;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Processes;

namespace ProcessExplorerWeb.Application.Processes.Common.Mappers
{
    public class ProcessSearchResponseDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ProcessSearchItem, ProcessSearchResponseDto>()
                .Map(dst => dst.ProcessName, src => src.ProcessName)
                .Map(dst => dst.OccuresInNumOfSessions, src => src.OccuresInNumOfSessions);
        }
    }
}
