using Mapster;
using ProcessExplorerWeb.Application.Session.SharedDtos;
using ProcessExplorerWeb.Core.Queries.Sessions;

namespace ProcessExplorerWeb.Application.Session.SharedMappings
{
    public class SessionMostActiveDayDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            {
                config.ForType<TopSessionDay, SessionMostActiveDayDto>()
                    .Map(dst => dst.Date, src => $"{src.Date.Day}/{src.Date.Month}/{src.Date.Year}")
                    .Map(dst => dst.NumberOfSessions, src => src.NumberOfSessions);
            }
        }
    }
}
