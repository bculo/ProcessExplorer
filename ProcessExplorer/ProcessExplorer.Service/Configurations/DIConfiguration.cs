using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Interfaces;
using ProcessExplorer.Service.Process;
using ProcessExplorer.Service.Services.System;

namespace ProcessExplorer.Service.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPlatformInformationService, SystemInformationService>();
            services.AddSingleton<IProcessCollectorFactory, ProcessCollectorFactory>();
            services.AddTransient<IPlatformProcessRecognizer, PlatformProcessRecognizer>();
        }
    }
}
