using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessExplorerWeb.Application.Extensions;
using ProcessExplorerWeb.Infrastructure.Identity;
using ProcessExplorerWeb.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure
{
    public static class DLLInfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureLayer(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIInstallationCofigurations(configuration, Assembly.GetExecutingAssembly());
            return services;
        }

        public static async Task ConfigureStorage(IServiceProvider provider)
        {
            //get database instance
            var dbContext = provider.GetRequiredService<ProcessExplorerDbContext>();

            //apply new migrations
            if (dbContext.Database.IsSqlServer())
                dbContext.Database.Migrate();

            //get services
            var userManager = provider.GetService<UserManager<IdentityAppUser>>();
            var roleManager = provider.GetService<RoleManager<IdentityAppRole>>();
            var configuration = provider.GetService<IConfiguration>();

            //seed database
            if (dbContext.Database.CanConnect())
                await ProcessExplorerDbContextSeed.SeedDatabase(configuration, userManager, roleManager);
            else
                throw new Exception("Cant connect to database");
        }
    }
}
