using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Options;

namespace ProcessExplorer.Service.Configurations
{
    public class OptionConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PlatformRecognizerOptions>(opt => configuration.GetSection(nameof(PlatformRecognizerOptions)).Bind(opt));
            services.Configure<InternetCheckOptions>(opt => configuration.GetSection(nameof(InternetCheckOptions)).Bind(opt));
        }
    }
}
