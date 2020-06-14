using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Options;

namespace ProcessExplorer.Service.Configurations
{
    public class RegexConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var searchRegex = configuration.GetSection(nameof(PlatformRecognizerOptions)).Get<PlatformRecognizerOptions>();
            RegexManager.AddRegex(nameof(searchRegex.WindowsRegex), searchRegex.WindowsRegex);
            RegexManager.AddRegex(nameof(searchRegex.LinuxRegex), searchRegex.LinuxRegex);
        }
    }
}
