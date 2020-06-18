using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorerWeb.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Configurations
{
    public class IdentityAppUserConfiguration : IEntityTypeConfiguration<IdentityAppUser>
    {
        public void Configure(EntityTypeBuilder<IdentityAppUser> builder)
        {
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.ProfilePicture)
                .IsRequired(false);

            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();


            builder.ToTable(name: nameof(IdentityAppUser), schema: "Security");
        }
    }
}
