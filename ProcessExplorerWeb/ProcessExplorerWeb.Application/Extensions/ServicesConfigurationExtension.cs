﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProcessExplorerWeb.Application.Extensions
{
    public static class ServicesConfigurationExtension
    {
        public static void ConfigureIInstallationCofigurations(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            var configs = assembly.ExportedTypes.Where(x =>
                        typeof(IInstallation).IsAssignableFrom(x)
                        && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance)
                    .Cast<IInstallation>();

            foreach (var config in configs)
                config.Configure(services, configuration);
        }
    }
}
