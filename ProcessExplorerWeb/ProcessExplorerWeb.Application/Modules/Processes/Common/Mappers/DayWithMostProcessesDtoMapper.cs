using Mapster;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Processes;

namespace ProcessExplorerWeb.Application.Processes.Common.Mappers
{
    public class DayWithMostProcessesDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TopProcessDay, DayWithMostProcessesDto>()
                .Map(dst => dst.Day, src => src.Day)
                .Map(dst => dst.TotalNumberOfDifferentProcesses, src => src.TotalNumberOfDifferentProcesses);
        }
    }
}
