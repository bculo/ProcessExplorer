using Mapster;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Core.Entities;

namespace ProcessExplorer.Application.MappingProfiles
{
    /// <summary>
    /// Profile for session entity
    /// </summary>
    public class SessionProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SessionInformation, Session>()
                .Map(dst => dst.Id, src => src.SessionId)
                .Map(dst => dst.Started, src => src.SessionStarted)
                .Map(dst => dst.UserName, src => src.User);

            config.ForType<Session, UserSessionDto>()
                .Map(dst => dst.SessionId, src => src.Id)
                .Map(dst => dst.StartTime, src => src.Started);
        }
    }
}
