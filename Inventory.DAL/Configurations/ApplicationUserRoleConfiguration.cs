using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.DAL.Configurations
{
   
    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("User_Role", "dbo");
          

        
        }
    }
}
