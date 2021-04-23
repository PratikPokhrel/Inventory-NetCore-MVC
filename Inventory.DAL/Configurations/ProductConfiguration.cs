using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable(name: "Product", schema: "dbo");
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasQueryFilter(e => !e.IsDeleted);


            builder.HasOne(e => e.Category)
                   .WithMany(e => e.Product)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.CreatedByUser)
                   .WithMany(e => e.CreatedProduct)
                   .HasForeignKey(e => e.CreatedBy)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.UpdatedByUser)
                   .WithMany(e => e.UpdatedProduct)
                   .HasForeignKey(e => e.UpdatedBy)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.DeletedByUser)
                    .WithMany(e => e.DeletedProduct)
                    .HasForeignKey(e => e.DeletedBy)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
