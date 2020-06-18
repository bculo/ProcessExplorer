using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure
{
    public static class DLLInfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureLayer(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIInstallationCofigurations(configuration, Assembly.GetExecutingAssembly());
            return services;
        }

        public static async Task ConfigureDatabase(IServiceProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
