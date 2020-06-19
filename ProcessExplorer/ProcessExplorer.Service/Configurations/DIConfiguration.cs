using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Application;
using ProcessExplorer.Service.Authentication;
using ProcessExplorer.Service.Clients;
using ProcessExplorer.Service.Interfaces;
using ProcessExplorer.Service.Log;
using ProcessExplorer.Service.Process;
using ProcessExplorer.Service.Services.System;
using ProcessExplorer.Service.Session;
using ProcessExplorer.Service.Time;
using System;

namespace ProcessExplorer.Service.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPlatformInformationService, SystemInformationService>();
            services.AddSingleton<IProcessCollectorFactory, ProcessCollectorFactory>();
            services.AddSingleton<IApplicationCollectorFactory, ApplicationCollectorFactory>();

            services.AddTransient<ILoggerWrapper, LoggerWrapper>();
            services.AddTransient<IUserSessionFactory, UserSessionFactory>();
            services.AddTransient<IPlatformProcessRecognizer, PlatformProcessRecognizer>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ITokenService, AuthenticationTokenService>();


            services.AddHttpClient<IInternet, InternetConnectionChecker>();
            services.AddHttpClient<IAuthenticationClient, AuthenticationClient>();
        }
    }
}
