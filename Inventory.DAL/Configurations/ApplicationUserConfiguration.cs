using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(name: "User", schema: "dbo");
            builder.Property(e => e.Email).HasMaxLength(50).IsRequired();
            builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.PhoneNumber).HasMaxLength(15);


            builder.HasMany(e => e.Roles)
                .WithOne(e=>e.ApplicationUser)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
