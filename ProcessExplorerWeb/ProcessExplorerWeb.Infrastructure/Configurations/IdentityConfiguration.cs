using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Infrastructure.Identity;
using ProcessExplorerWeb.Infrastructure.Persistence;

namespace ProcessExplorerWeb.Infrastructure.Configurations
{
    public class IdentityConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<IdentityAppUser, IdentityAppRole>()
                .AddEntityFrameworkStores<ProcessExplorerDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
