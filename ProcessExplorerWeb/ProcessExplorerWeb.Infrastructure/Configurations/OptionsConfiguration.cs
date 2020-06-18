﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Infrastructure.Options;

namespace ProcessExplorerWeb.Infrastructure.Configurations
{
    public class OptionsConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            services.Configure<AuthenticationOptions>(opt => config.GetSection(nameof(AuthenticationOptions)).Bind(opt));
            return services;
        }
    }
}
