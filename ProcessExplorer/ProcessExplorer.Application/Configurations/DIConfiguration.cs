﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Behaviours;
using ProcessExplorer.Application.Common.Interfaces;

namespace ProcessExplorer.Application.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationCollectorBehaviour, ApplicationCollectorBehaviour>();
            services.AddScoped<IProcessBehaviour, ProcessCollectorBehaviour>();
            services.AddScoped<IUpdateBehaviour, UpdateServerBehaviour>();
        }
    }
}