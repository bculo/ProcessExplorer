using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Behaviours.Login;
using ProcessExplorer.Interfaces;

namespace ProcessExplorer.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IStartupPoint, LoginEveryTimeBehaviour>();
        }
    }
}
