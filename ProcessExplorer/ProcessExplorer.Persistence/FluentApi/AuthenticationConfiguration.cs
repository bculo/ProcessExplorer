using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence.FluentApi
{
    public class AuthenticationConfiguration : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.Property(i => i.Content)
                .HasMaxLength(1000);

            builder.ToTable(nameof(Authentication));
        }
    }
}
