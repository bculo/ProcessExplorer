using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Options;

namespace ProcessExplorer.Application.Configurations
{
    public class OptionConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProcessCollectorOptions>(opt => configuration.GetSection(nameof(ProcessCollectorOptions)).Bind(opt));
        }
    }
}
