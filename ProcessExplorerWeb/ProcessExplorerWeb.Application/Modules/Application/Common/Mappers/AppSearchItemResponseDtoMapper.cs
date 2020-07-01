using Mapster;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Common.Mappers
{
    public class AppSearchItemResponseDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ApplicationSearchItem, AppSearchItemResponseDto>()
                 .Map(dst => dst.ApplicationName, src => src.ApplicationName)
                 .Map(dst => dst.OccuresNumOfTime, src => src.OccuresNumOfTime);
        }
    }
}
