using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence.FluentApi
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.Property(i => i.ApplicationName)
                .HasMaxLength(300)
                .IsRequired();

            builder.ToTable(nameof(ApplicationEntity));
        }
    }
}
