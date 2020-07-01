using Mapster;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using ProcessExplorerWeb.Core.Queries.Sessions;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorerWeb.Application.Session.SharedMappings
{
    public class SessionLineChartDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IEnumerable<SessionDateChartItem>, SessionLineChartDto>()
                .Map(dst => dst.Date, src => src.Select(i => $"{i.Date.Day}/{i.Date.Month}/{i.Date.Year}"))
                .Map(dst => dst.Number, src => src.Select(i => i.Number));
        }
    }
}
