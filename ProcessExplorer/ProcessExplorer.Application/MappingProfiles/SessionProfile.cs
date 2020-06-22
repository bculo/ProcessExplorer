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
            //SessionInformation -> Session
            config.ForType<SessionInformation, Session>()
                .Map(dst => dst.Id, src => src.SessionId)
                .Map(dst => dst.Started, src => src.SessionStarted)
                .Map(dst => dst.UserName, src => src.User);

            //Session -> UserSessionDto
            config.ForType<Session, UserSessionDto>()
                .Map(dst => dst.SessionId, src => src.Id)
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.Applications, src => src.Applications)
                .Map(dst => dst.Processes, src => src.ProcessEntities);

            //Session -> UserSessionDto
            config.ForType<SessionInformation, UserSessionDto>()
                .Map(dst => dst.SessionId, src => src.SessionId)
                .Map(dst => dst.Started, src => src.SessionStarted)
                .Map(dst => dst.UserName, src => src.User)
                .Map(dst => dst.OS, src => src.OS);
        }
    }
}
