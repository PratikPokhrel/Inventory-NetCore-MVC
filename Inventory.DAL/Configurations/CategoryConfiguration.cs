using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(name: "Category", schema: "dbo");
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

            builder.HasQueryFilter(e => !e.IsDeleted);
           
        }
    }
}
