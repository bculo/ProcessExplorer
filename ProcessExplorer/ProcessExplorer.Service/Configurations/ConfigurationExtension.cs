using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProcessExplorer.Service.Configurations
{
    public static class ConfigurationExtension
    {
        public static void ApplyConfigurationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.ApplyAssemblyConfigration(configuration, Assembly.GetExecutingAssembly());
        }
    }
}
