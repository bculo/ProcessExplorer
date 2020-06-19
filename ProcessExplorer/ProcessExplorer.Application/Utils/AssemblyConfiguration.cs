using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace ProcessExplorer.Application.Utils
{
    public static class AssemblyConfiguration
    {
        public static void ApplyAssemblyConfigration(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            var configs = assembly.ExportedTypes.Where(x =>
                        typeof(IInstallation).IsAssignableFrom(x)
                        && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance)
                    .Cast<IInstallation>();

            foreach (var config in configs)
                config.Install(services, configuration);
        }
    }
}
