using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;

namespace ProcessExplorer.Persistence.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
