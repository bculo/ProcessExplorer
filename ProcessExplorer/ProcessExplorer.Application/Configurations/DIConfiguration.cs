using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ILoginBehaviour, LoginConsoleBehaviour>();
        }
    }
}
