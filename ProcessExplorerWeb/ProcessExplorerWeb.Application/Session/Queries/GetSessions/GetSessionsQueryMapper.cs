using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Session;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSessions
{
    public class GetSessionsQueryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<SessionDetailedModel, GetSessionsQueryResponseDto>()
                .Map(dst => dst.ApplicationNumber, src => src.ApplicationNum)
                .Map(dst => dst.DifferentProcessesNumber, src => src.DifferentProcesses)
                .Map(dst => dst.Id, src => src.SessionId)
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Started, src => src.Started);
        }
    }
}
