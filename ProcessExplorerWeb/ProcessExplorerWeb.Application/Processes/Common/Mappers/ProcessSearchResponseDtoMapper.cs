using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Process;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;

namespace ProcessExplorerWeb.Application.Processes.Common.Mappers
{
    public class ProcessSearchResponseDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ProcessSearchModel, ProcessSearchResponseDto>()
                .Map(dst => dst.ProcessName, src => src.ProcessName)
                .Map(dst => dst.OccuresInNumOfSessions, src => src.OccuresInNumOfSessions);
        }
    }
}
