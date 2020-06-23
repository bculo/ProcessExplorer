using Mapster;
using Microsoft.VisualBasic;
using ProcessExplorer.Application.Common.Models;
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
                .Map(dst => dst.Name, src => src.ProcessName)
                .Map(dst => dst.Detected, src => src.Saved);

            //ProcessInformation -> ProcessEntity
            config.ForType<ProcessInformation, ProcessEntity>()
                .Map(dst => dst.Saved, src => src.Fetched)
                .Map(dst => dst.SessionId, src => src.Session)
                .Ignore(dest => dest.Session);

            //ProcessInformation -> ProcessEntity
            config.ForType<ProcessInformation, ProcessDto>()
                .Map(dst => dst.Name, src => src.ProcessName);
        }
    }
}
