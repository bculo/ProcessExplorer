using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Extensions;
using System.Reflection;

namespace ProcessExplorerWeb.Application
{
    public static class DLLApplicationConfiguration
    {
        public static IServiceCollection AddApplicationLayer(
             this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIInstallationCofigurations(configuration, Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
