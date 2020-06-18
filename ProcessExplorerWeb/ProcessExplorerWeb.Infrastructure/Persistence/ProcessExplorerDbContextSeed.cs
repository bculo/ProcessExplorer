using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProcessExplorerWeb.Application.Constants;
using ProcessExplorerWeb.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Persistence
{
    public static class ProcessExplorerDbContextSeed
    {
        public async static Task SeedDatabase(IConfiguration config, UserManager<IdentityAppUser> userManager, RoleManager<IdentityAppRole> roleManager)
        {
            await AddRoles(config, roleManager);
            await AddApplicationAdmin(config, userManager, roleManager);
        }

        private async static Task AddRoles(IConfiguration config, RoleManager<IdentityAppRole> roleManager)
        {
            if (await roleManager.Roles.CountAsync() > 0)
                return;

            IEnumerable<IdentityAppRole> roles = config.GetSection(nameof(IdentityAppRole)).Get<IdentityAppRole[]>();

            foreach (var role in roles)
            {
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                    throw new Exception("Role seed exception");
            }
        }

        private async static Task AddApplicationAdmin(IConfiguration config, UserManager<IdentityAppUser> userManager, RoleManager<IdentityAppRole> roleManager)
        {
            IdentityAppUser admin = config.GetSection(nameof(IdentityAppUser)).Get<IdentityAppUser>();

            var adminTask = await userManager.Users.AllAsync(u => u.UserName != admin.UserName);
            var roleTask = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == RoleConstants.ADMIN);

            if (!adminTask)
                return;

            admin.UserRoles.Add(new IdentityAppUserRole { Role = roleTask });
            IdentityResult result = await userManager.CreateAsync(admin, nameof(admin));

            if (!result.Succeeded)
                throw new Exception("Admin seed exception");
        }
    }
}
