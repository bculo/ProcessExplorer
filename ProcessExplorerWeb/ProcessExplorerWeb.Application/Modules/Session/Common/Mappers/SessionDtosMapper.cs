using Mapster;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using ProcessExplorerWeb.Core.Queries.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Session.Common.Mappers
{
    public class SessionDtosMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IEnumerable<SessionDateChartItem>, SessionLineChartDto>()
                .Map(dst => dst.Date, src => src.Select(i => $"{i.Date.Day}/{i.Date.Month}/{i.Date.Year}"))
                .Map(dst => dst.Number, src => src.Select(i => i.Number));

            config.ForType<TopSessionDay, SessionMostActiveDayDto>()
                .Map(dst => dst.Date, src => $"{src.Date.Day}/{src.Date.Month}/{src.Date.Year}")
                .Map(dst => dst.NumberOfSessions, src => src.NumberOfSessions);
        }
    }
}
