using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Configurations
{
    public class DatabaseConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ProcessExplorerDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString(nameof(ProcessExplorerDbContext))));

            return services;
        }
    }
}
