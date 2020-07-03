using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Configurations
{
    public class MapperConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            var adapter = TypeAdapterConfig.GlobalSettings;
            IList<IRegister> profiles = adapter.Scan(Assembly.GetExecutingAssembly());
            adapter.Apply(profiles);

            return services;
        }
    }
}
