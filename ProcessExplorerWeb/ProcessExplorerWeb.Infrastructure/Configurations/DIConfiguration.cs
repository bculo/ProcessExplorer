using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Interfaces;
using ProcessExplorerWeb.Infrastructure.Identity.Services;
using ProcessExplorerWeb.Infrastructure.Interfaces;
using ProcessExplorerWeb.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            #region SCOPED

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            #endregion

            #region TRANSIENT

            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IDateTime, DateTimeService>();

            #endregion

            #region SINGLETON



            #endregion

            return services;
        }
    }
}
