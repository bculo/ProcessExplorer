using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Api.Services;
using ProcessExplorer.Api.SignalR;
using ProcessExplorerWeb.Application.Common.Contracts.Notifications;
using ProcessExplorerWeb.Application.Common.Interfaces;

namespace ProcessExplorer.Api.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddSingleton(typeof(IConnectionMapper<>), typeof(ConnectionMapper<>));

            return services;
        }
    }
}
