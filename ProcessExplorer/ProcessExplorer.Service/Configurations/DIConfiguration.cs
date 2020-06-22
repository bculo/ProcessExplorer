using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Application;
using ProcessExplorer.Service.Authentication;
using ProcessExplorer.Service.Clients;
using ProcessExplorer.Service.Log;
using ProcessExplorer.Service.Process;
using ProcessExplorer.Service.Services.System;
using ProcessExplorer.Service.Session;
using ProcessExplorer.Service.Time;

namespace ProcessExplorer.Service.Configurations
{
    public class DIConfiguration : IInstallation
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPlatformInformationService, SystemInformationService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<ITokenService, AuthenticationTokenService>();

            services.AddTransient<IProcessCollectorFactory, ProcessCollectorFactory>();
            services.AddTransient<IApplicationCollectorFactory, ApplicationCollectorFactory>();
            services.AddTransient<IUserSessionFactory, UserSessionFactory>();
            services.AddTransient<ILoggerWrapper, LoggerWrapper>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ISynchronizationClientFactory, SyncClientFactory>();

            services.AddHttpClient();
            services.AddHttpClient<IInternet, InternetConnectionChecker>();
            services.AddHttpClient<IAuthenticationClient, AuthenticationClient>();
        }
    }
}
