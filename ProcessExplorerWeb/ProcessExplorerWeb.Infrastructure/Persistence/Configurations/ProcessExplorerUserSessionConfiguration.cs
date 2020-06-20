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
                .ValueGeneratedNever();

            builder.Property(i => i.UserName)
                .HasMaxLength(300)
                .IsRequired();

            builder.ToTable(nameof(ProcessExplorerUserSession));
        }            
    }
}
