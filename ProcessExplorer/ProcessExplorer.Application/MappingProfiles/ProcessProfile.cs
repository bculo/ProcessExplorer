using Mapster;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Core.Entities;

namespace ProcessExplorer.Application.MappingProfiles
{
    public class ProcessProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Session -> UserSessionDto
            config.ForType<ProcessEntity, ProcessDto>()
                .Map(dst => dst.Name, src => src.ProcessName);
        }
    }
}
