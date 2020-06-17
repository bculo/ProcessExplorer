using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence
{
    /// <summary>
    /// Use this on test environment only
    /// </summary>
    public static class ProcessDbContextHelper
    {

        /// <summary>
        /// Seed data to database
        /// </summary>
        public static void SeedData()
        {

        }

        /// <summary>
        /// Applly all migration
        /// </summary>
        /// <param name="provider"></param>
        public static void ApplyMigrations(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ProcessExplorerDbContext>();
            context.Database.Migrate();
        }
    }
}
