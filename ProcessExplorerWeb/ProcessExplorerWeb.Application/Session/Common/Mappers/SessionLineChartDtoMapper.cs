﻿using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Session;
using ProcessExplorerWeb.Application.Session.SharedDtos;

namespace ProcessExplorerWeb.Application.Session.SharedMappings
{
    public class SessionLineChartDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SessionChartLineModel, SessionLineChartDto>()
                .Map(dst => dst.Date, src => src.Date)
                .Map(dst => dst.Number, src => src.Number);
        }
    }
}