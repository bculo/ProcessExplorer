using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence
{
    /// <summary>
    /// Desing time factory
    /// </summary>
    public class ProcessExplorerContextFactory : IDesignTimeDbContextFactory<ProcessExplorerDbContext>
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
            builder.UseSqlite("Data Source=processexplorer.db");

            return new ProcessExplorerDbContext(builder.Options);
        }
    }
}
