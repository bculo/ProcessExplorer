using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Notifications;
using ProcessExplorer.Behaviours.Login;
using ProcessExplorer.Interfaces;
using ProcessExplorer.Services;

namespace ProcessExplorer.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStartupPoint, LoginEveryTimeBehaviour>();
            services.AddScoped<INotificationService, ConsoleNotificationService>();
        }
    }
}
