using Mapster;
using ProcessExplorerWeb.Application.Common.Dtos;
using ProcessExplorerWeb.Application.Common.Models.Charts;

namespace ProcessExplorerWeb.Application.Common.Mappings
{
    public class ColumnChartMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ColumnChartStatisticModel, ColumnChartDto>()
                .Map(dst => dst.Label, src => src.Label)
                .Map(dst => dst.Value, src => src.Value);
        }
    }
}
