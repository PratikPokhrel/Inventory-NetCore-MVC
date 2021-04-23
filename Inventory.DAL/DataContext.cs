using Inventory.DAL.Configurations;
using Inventory.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.DAL
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private string loggedInUserId;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
          
        }

      
        public virtual DbSet<Product> Product  { get; set; }
        public virtual DbSet<Category> Category { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Auth Entities Configuration
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserTokenConfiguration());

            //Other Entities Configuration
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());


            // base.OnModelCreating(modelBuilder);

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken token = default)
        {
            var entries = ChangeTracker.Entries()
                                               .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
            loggedInUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    if (((AuditableEntity)entityEntry.Entity).IsDeleted)
                    {
                        ((AuditableEntity)entityEntry.Entity).DeletedOn = DateTime.Now;
                        ((AuditableEntity)entityEntry.Entity).DeletedBy = Guid.Parse(loggedInUserId);
                    }
                    else
                    {
                        ((AuditableEntity)entityEntry.Entity).UpdatedOn = DateTime.Now;
                        ((AuditableEntity)entityEntry.Entity).UpdatedBy = Guid.Parse(loggedInUserId);
                    }

                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).CreatedOn = DateTime.Now;
                    ((AuditableEntity)entityEntry.Entity).CreatedBy = Guid.Parse(loggedInUserId);
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
        }
    }

    
}
