using Mapster;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Core.Entities;
using System;

namespace ProcessExplorer.Application.MappingProfiles
{
    public class ApplicationProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //ApplicationInformation -> ApplicationEntity
            config.ForType<ApplicationInformation, ApplicationEntity>()
                .Map(dst => dst.SessionId, src => src.Session)
                .Map(dst => dst.Saved, src => src.FetchTime)
                .Ignore(i => i.Session);

            //ApplicationInformation -> ApplicationDto
            config.ForType<ApplicationInformation, ApplicationDto>()
                .Map(dst => dst.SessionId, src => src.Session)
                .Map(dst => dst.Started, src => src.StartTime)
                .Map(dst => dst.LastUse, src => src.FetchTime)
                .Map(dst => dst.Name, src => src.ApplicationName);

            //ApplicationEntity -> ApplicationDto
            config.ForType<ApplicationEntity, ApplicationDto>()
                .Map(dst => dst.Started, src => src.StartTime)
                .Map(dst => dst.LastUse, src => src.Saved)
                .Map(dst => dst.Name, src => src.ApplicationName)
                .Map(dst => dst.SessionId, src => src.SessionId);
        }
    }
}
