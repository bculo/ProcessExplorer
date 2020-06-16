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
            services.Configure<LoggerOptions>(opt => configuration.GetSection(nameof(LoggerOptions)).Bind(opt));
            services.Configure<ApplicationCollectorUsageOptions>(opt => configuration.GetSection(nameof(ApplicationCollectorUsageOptions)).Bind(opt));
            services.Configure<ProcessCollectorUsageOptions>(opt => configuration.GetSection(nameof(ProcessCollectorUsageOptions)).Bind(opt));
        }
    }
}
