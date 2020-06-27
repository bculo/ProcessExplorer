using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Process;

namespace ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses
{
    public class DayWithMostProcessesQueryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TopProcessDayModel, DayWithMostProcessesQueryResponseDto>()
                .Map(dst => dst.Day, src => src.Day)
                .Map(dst => dst.TotalNumberOfDifferentProcesses, src => src.TotalNumberOfDifferentProcesses);
        }
    }
}
