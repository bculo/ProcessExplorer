using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Configurations
{
    public class ProcessExplorerUserConfiguration : IEntityTypeConfiguration<ProcessExplorerUser>
    {
        public void Configure(EntityTypeBuilder<ProcessExplorerUser> builder)
        {
            builder.HasIndex(i => i.UserName)
                .IsUnique();

            builder.Property(i => i.UserName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(i => i.Email)
                .IsUnique();

            builder.Property(i => i.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasMany(i => i.Sessions)
                .WithOne(i => i.ProcessExplorerUser)
                .HasForeignKey(i => i.ExplorerUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(ProcessExplorerUser));
        }
    }
}
