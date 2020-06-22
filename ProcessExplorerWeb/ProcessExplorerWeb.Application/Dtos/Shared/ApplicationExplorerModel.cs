using Mapster;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Dtos.Shared
{
    /// <summary>
    /// Base model of every process DTO instance
    /// Every process needs to have this defiend properties
    /// </summary>
    public abstract class ApplicationExplorerModel
    {
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime LastUse { get; set; }
    }

    public class ApplicationExplorerModelProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ApplicationExplorerModel, ApplicationEntity>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.Name, src => src.Name)
                .Map(dst => dst.Closed, src => src.LastUse);
        }
    }
}
