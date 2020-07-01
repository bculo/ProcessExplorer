using Mapster;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Core.Queries.Charts;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorerWeb.Application.Common.Mappings
{
    public class PieChartMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IEnumerable<PieChartItem>, PieChartDto>()
                .Map(dst => dst.Name, src => src.Select(i => i.ChunkName))
                .Map(dst => dst.Quantity, src => src.Select(i => i.Quantity));
        }
    }
}
