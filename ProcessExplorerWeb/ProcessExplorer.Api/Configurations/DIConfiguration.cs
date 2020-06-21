﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Api.Services;
using ProcessExplorerWeb.Application.Common.Interfaces;

namespace ProcessExplorer.Api.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
