using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Configurations
{


    public class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            builder.ToTable("Role_Claim", "dbo");
            builder.Property(e => e.Id);
            builder.Property(e => e.RoleId);
            builder.Property(e => e.ClaimValue).HasMaxLength(50);
            builder.Property(e => e.ClaimType).HasMaxLength(50);
        }
    }
}


