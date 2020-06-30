using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Process;
using ProcessExplorerWeb.Application.Processes.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Common.Mappers
{
    public class DayWithMostProcessesDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TopProcessDayModel, DayWithMostProcessesDto>()
                .Map(dst => dst.Day, src => src.Day)
                .Map(dst => dst.TotalNumberOfDifferentProcesses, src => src.TotalNumberOfDifferentProcesses);
        }
    }
}
