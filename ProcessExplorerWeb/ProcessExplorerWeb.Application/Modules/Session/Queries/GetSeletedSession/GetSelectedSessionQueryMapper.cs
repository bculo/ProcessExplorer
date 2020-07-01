using Mapster;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSeletedSession
{
    public class GetSelectedSessionQueryMapper
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ProcessExplorerUserSession, GetSelectedSessionQueryResponseDto>()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Started, src => src.Started);
        }
}
}
