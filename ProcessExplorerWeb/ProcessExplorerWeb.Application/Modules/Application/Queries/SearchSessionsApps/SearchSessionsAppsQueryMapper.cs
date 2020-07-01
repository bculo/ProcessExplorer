using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;
using ProcessExplorerWeb.Core.Queries.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Queries.SearchSessionsApps
{
    public class SearchSessionsAppsQueryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ApplicationSearchDetail, SearchSessionsAppsQueryResponseDto>()
                .Map(dst => dst.Closed, src => src.Closed)
                .Map(dst => dst.Opened, src => src.Opened)
                .Map(dst => dst.Name, src => src.Name);
        }
    }
}
