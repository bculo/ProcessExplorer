using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorerWeb.Core.Entities;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Configurations
{
    public class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).HasMaxLength(300)
                .IsRequired();

            builder.HasOne(i => i.Session)
                .WithMany(i => i.Applications)
                .HasForeignKey(i => i.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.ToTable(nameof(ApplicationEntity));
        }
    }
}
