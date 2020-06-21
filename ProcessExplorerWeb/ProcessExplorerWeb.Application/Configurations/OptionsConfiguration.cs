using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;

namespace ProcessExplorerWeb.Application.Configurations
{
    public class OptionsConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            services.Configure<PerformanceOptions>(opt => config.GetSection(nameof(PerformanceOptions)).Bind(opt));

            return services;
        }
    }
}
