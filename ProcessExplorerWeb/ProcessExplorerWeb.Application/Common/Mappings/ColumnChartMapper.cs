using Mapster;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Models.Charts;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorerWeb.Application.Common.Mappings
{
    public class ColumnChartMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IEnumerable<ColumnChartStatisticModel>, ColumnChartDto>()
                .Map(dst => dst.Label, src => src.Select(i => i.Label))
                .Map(dst => dst.Value, src => src.Select(i => i.Value));
        }
    }
}
