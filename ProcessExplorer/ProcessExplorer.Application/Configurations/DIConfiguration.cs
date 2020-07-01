using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Behaviours;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Behaviours;

namespace ProcessExplorer.Application.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationCollectorBehaviour, ApplicationCollectorBehaviour>();
            services.AddScoped<IProcessBehaviour, ProcessCollectorBehaviour>();
            services.AddScoped<ISyncBehaviour, SyncBehaviour>();
            services.AddScoped<ICommunicationTypeBehaviour, CommunicationTypeCheckBehaviour>();
        }
    }
}
