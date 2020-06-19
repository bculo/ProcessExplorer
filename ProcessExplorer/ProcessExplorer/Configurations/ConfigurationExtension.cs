using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Utils;
using System.Reflection;

namespace ProcessExplorer.Configurations
{
    public static class ConfigurationExtension
    {
        public static void ApplyConfigurationConsoleApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AssemblyConfiguration.ApplyAssemblyConfigration(services, configuration, Assembly.GetExecutingAssembly());
        }
    }
}
