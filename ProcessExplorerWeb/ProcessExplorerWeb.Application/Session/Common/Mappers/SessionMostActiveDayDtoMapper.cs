using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Session;
using ProcessExplorerWeb.Application.Session.SharedDtos;

namespace ProcessExplorerWeb.Application.Session.SharedMappings
{
    public class SessionMostActiveDayDtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            {
                config.ForType<PopularSessionDayModel, SessionMostActiveDayDto>()
                    .Map(dst => dst.Date, src => src.Date)
                    .Map(dst => dst.NumberOfSessions, src => src.NumberOfSessions);
            }
        }
    }
}
