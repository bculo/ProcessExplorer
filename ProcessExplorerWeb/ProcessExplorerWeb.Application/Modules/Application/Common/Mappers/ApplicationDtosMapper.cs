using Mapster;
using ProcessExplorerWeb.Application.Modules.Application.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Common.Mappers
{
    public class ApplicationDtosMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TopApplicationDay, DayWithMostAppsDto>()
                .Map(dst => dst.Day, src => src.Day)
                .Map(dst => dst.Number, src => src.Number);

            config.ForType<ApplicationSearchItem, AppSearchItemResponseDto>()
                 .Map(dst => dst.ApplicationName, src => src.ApplicationName)
                 .Map(dst => dst.OccuresNumOfTime, src => src.OccuresNumOfTime);

            config.ForType<IEnumerable<AppSessionLineChartItem>, AppSessionLineChartDto> ()
                 .Map(dst => dst.Date, src => src.Select(i => $"{i.Date.Day}/{i.Date.Month}/{i.Date.Year}"))
                 .Map(dst => dst.Number, src => src.Select(i => i.Number));
        }
    }
}
