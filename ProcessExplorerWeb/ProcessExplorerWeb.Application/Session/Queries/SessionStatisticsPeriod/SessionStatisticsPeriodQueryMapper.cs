using Mapster;
using ProcessExplorerWeb.Application.Common.Charts.Shared;
using ProcessExplorerWeb.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsPeriod
{
    public class SessionStatisticsAllQueryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<PieChartStatisticModel, PieChartDto>()
                .Map(dst => dst.Name, src => src.ChunkName)
                .Map(dst => dst.Quantity, src => src.Quantity);
        }
    }
}
