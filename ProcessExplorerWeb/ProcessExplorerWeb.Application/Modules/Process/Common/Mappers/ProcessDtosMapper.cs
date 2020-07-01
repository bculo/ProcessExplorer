using Mapster;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Processes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Processes.Common.Mappers
{
    public class ProcessDtosMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TopProcessDay, DayWithMostProcessesDto>()
                .Map(dst => dst.Day, src => src.Day)
                .Map(dst => dst.TotalNumberOfDifferentProcesses, src => src.TotalNumberOfDifferentProcesses);

            config.ForType<ProcessSearchItem, ProcessSearchResponseDto>()
                .Map(dst => dst.ProcessName, src => src.ProcessName)
                .Map(dst => dst.OccuresInNumOfSessions, src => src.OccuresInNumOfSessions);
        }
    }
}
