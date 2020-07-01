using Mapster;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Common.Mappers
{
    public class DayWithMostApplicationDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TopApplicationDay, DayWithMostAppsDto>()
                .Map(dst => dst.Day, src => src.Day)
                .Map(dst => dst.Number, src => src.Number);
        }
    }
}
