using Mapster;
using ProcessExplorerWeb.Application.Common.Charts.Shared;
using ProcessExplorerWeb.Application.Common.Dtos;

namespace ProcessExplorerWeb.Application.Common.Mappings
{
    public class PieChartMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<PieChartStatisticModel, PieChartDto>()
                .Map(dst => dst.Name, src => src.ChunkName)
                .Map(dst => dst.Quantity, src => src.Quantity);
        }
    }
}
