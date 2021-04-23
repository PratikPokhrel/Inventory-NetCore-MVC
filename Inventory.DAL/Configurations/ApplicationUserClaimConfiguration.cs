using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Configurations
{
    

    public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
        {
            builder.ToTable("User_Claim", "dbo");
            builder.Property(e => e.ClaimValue).HasMaxLength(50);
            builder.Property(e => e.ClaimType).HasMaxLength(50);

        }
    }
}

