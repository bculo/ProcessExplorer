using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Background;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Service.Configurations
{
    public class BackGroundServiceConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            #region BACKGROUND SERVICES

            services.AddHostedService<ProcessCollectorHostedService>();
            services.AddHostedService<ApplicationCollectorHostedService>();
            services.AddHostedService<SyncHostedService>();

            #endregion
        }
    }
}
