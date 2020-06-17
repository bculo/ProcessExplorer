using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ProcessExplorer.Persistence
{
    /// <summary>
    /// Type -> SQLite
    /// DB Context for console applicaiton
    /// </summary>
    public class ProcessExplorerDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Authentication> Authentications { get; set; }

        public ProcessExplorerDbContext(DbContextOptions<ProcessExplorerDbContext> options) : base(options)
        {
            Console.WriteLine("---------------------CONTEXT-------------------------");
            Console.WriteLine(JsonObjectDump.Dump(options));
        }

        /// <summary>
        /// Define that we use Sqlite
        /// DBContext configuration moved to DatabaseConfiguration.cs (we can use IConfiguration there)
        /// </summary>
        /// <param name="options"></param>
        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=processexplorer.db");

        /// <summary>
        /// Define entity relationship (Relationships are defiend with FluentApi)
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //does fluent api work with sqlite ??? :(((
            //oh boi it works :DDDD
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ProcessExplorerDbContext).Assembly);
        }
    }
}
