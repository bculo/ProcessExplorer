using Mapster;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Charts;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorerWeb.Application.Common.Mappings
{
    public class ColumnChartMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IEnumerable<ColumnChartItem>, ColumnChartDto>()
                .Map(dst => dst.Label, src => src.Select(i => i.Label))
                .Map(dst => dst.Value, src => src.Select(i => i.Value));
        }
    }
}
