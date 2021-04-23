using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Configurations
{

    public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
        {
            builder.ToTable("User_Login", "dbo");
            builder.HasKey(e => e.UserId);
        }
    }
}
