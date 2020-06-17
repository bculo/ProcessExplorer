using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Persistence.FluentApi
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(i => i.UserName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(i => i.ProcessEntities)
                .WithOne(p => p.Session)
                .HasForeignKey(i => i.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(i => i.Applications)
                .WithOne(p => p.Session)
                .HasForeignKey(i => i.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.ToTable(nameof(Session));
        }
    }
}
