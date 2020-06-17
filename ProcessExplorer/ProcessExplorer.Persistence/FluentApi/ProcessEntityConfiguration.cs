using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence.FluentApi
{
    public class ProcessEntityConfiguration : IEntityTypeConfiguration<ProcessEntity>
    {
        public void Configure(EntityTypeBuilder<ProcessEntity> builder)
        {
            builder.Property(i => i.ProcessName)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
