using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProcessExplorerWeb.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure
{
    public class ProcessExplorerDbContextFactory : IDesignTimeDbContextFactory<ProcessExplorerDbContext>
    {
        public ProcessExplorerDbContext CreateDbContext(string[] args)
        {
            /*
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettingspersitence.json", false)
                .Build();
            
            var connection = configuration.GetConnectionString("ProcessExplorerConnection");
            */

            var builder = new DbContextOptionsBuilder<ProcessExplorerDbContext>();
            builder.UseSqlServer("Data Source=DESKTOP-R7G16RS;Initial Catalog=ProcessExplorer;Integrated Security=True");

            return new ProcessExplorerDbContext(builder.Options);
        }
    }
}
