using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Behaviours;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System.Reflection;

namespace ProcessExplorerWeb.Application.Configurations
{
    public class MediatRConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            //Use MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Configure pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));

            return services;
        }
    }
}
