﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Infrastructure.Identity;
using System;

namespace ProcessExplorerWeb.Infrastructure.Persistence
{
    public class ProcessExplorerDbContext : IdentityDbContext<
        IdentityAppUser, IdentityAppRole, Guid,
        IdentityUserClaim<Guid>, IdentityAppUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ProcessExplorerDbContext(DbContextOptions<ProcessExplorerDbContext> options) : base(options)
        {
        }

        public DbSet<ProcessExplorerUser> ExplorerUsers { get; set; }
        public DbSet<ProcessExplorerUserSession> Sessions { get; set; }
        public DbSet<ProcessEntity> Processes { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //complex configuration -> Add to Persistence/Configurations
            builder.ApplyConfigurationsFromAssembly(typeof(ProcessExplorerDbContext).Assembly);

            //Identity tables names and schema
            builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable(name: "IdentityUserClaim", schema: "Security"));
            builder.Entity<IdentityAppUserRole>(entity => entity.ToTable(name: nameof(IdentityAppUserRole), schema: "Security"));
            builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable(name: "IdentityUserLogin", schema: "Security"));
            builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable(name: "IdentityRoleClaim", schema: "Security"));
            builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable(name: "IdentityUserToken", schema: "Security"));
        }
    }
}
