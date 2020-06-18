using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessExplorerWeb.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Configurations
{
    public class IdentityAppRoleConfiguration : IEntityTypeConfiguration<IdentityAppRole>
    {
        public void Configure(EntityTypeBuilder<IdentityAppRole> builder)
        {
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.ToTable(name: nameof(IdentityAppRole), schema: "Security");
        }
    }
}
