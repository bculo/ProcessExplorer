using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace ProcessExplorer.Service.Configurations
{
    public class MapperConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            IList<IRegister> profiles = config.Scan(Assembly.GetExecutingAssembly());
            config.Apply(profiles);
        }
    }
}
