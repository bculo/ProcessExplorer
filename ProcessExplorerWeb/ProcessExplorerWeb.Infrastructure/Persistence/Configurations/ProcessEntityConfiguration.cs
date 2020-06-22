using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Configurations
{
    public class ProcessEntityConfiguration : IEntityTypeConfiguration<ProcessEntity>
    {
        public void Configure(EntityTypeBuilder<ProcessEntity> builder)
        {
            builder.Property(i => i.ProcessName).HasMaxLength(300)
                .IsRequired();

            builder.HasOne(i => i.Session)
                .WithMany(i => i.Processes)
                .HasForeignKey(i => i.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.ToTable(nameof(ProcessEntity));
        }
    }
}
