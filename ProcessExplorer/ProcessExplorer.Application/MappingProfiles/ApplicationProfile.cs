using Mapster;
using ProcessExplorer.Application.Common.Models;
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
                .Map(dst => dst.Saved, src => DateTime.Now);
        }
    }
}
