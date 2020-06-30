using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Configurations
{
    public class ProcessExplorerUserSessionConfiguration : IEntityTypeConfiguration<ProcessExplorerUserSession>
    {
        public void Configure(EntityTypeBuilder<ProcessExplorerUserSession> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.ComputerSessionId)
                .IsRequired();

            builder.Property(i => i.UserName)
                .HasMaxLength(300)
                .IsRequired();

            builder.HasIndex(c => new { c.ComputerSessionId, c.ExplorerUserId }).IsUnique();

            builder.ToTable(nameof(ProcessExplorerUserSession));
        }            
    }
}
