using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Session;
using ProcessExplorerWeb.Application.Session.Queries.GetSessions;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
