using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Configurations
{

    public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
            builder.ToTable("User_Token", "dbo");
            builder.HasKey(e => e.UserId);

        }
    }
}
