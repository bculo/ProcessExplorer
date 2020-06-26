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
            services.Configure<AuthenticationOptions>(opt => config.GetSection(nameof(AuthenticationOptions)).Bind(opt));
            services.Configure<PaginationOptions>(opt => config.GetSection(nameof(PaginationOptions)).Bind(opt));
            services.Configure<DateTimePeriodOptions>(opt => config.GetSection(nameof(DateTimePeriodOptions)).Bind(opt));

            return services;
        }
    }
}
