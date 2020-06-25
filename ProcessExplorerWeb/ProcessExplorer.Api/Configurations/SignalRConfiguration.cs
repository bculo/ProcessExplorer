using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Configurations
{
    public class SignalRConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            services.AddSignalR();
            return services;
        }
    }
}
