using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.DAL.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            CreatedProduct= new HashSet<Product>();
            UpdatedProduct= new HashSet<Product>();
            DeletedProduct= new HashSet<Product>();

            Roles = new HashSet<ApplicationUserRole>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ApplicationUserRole> Roles { get; set; }

        public virtual ICollection<Product> CreatedProduct { get; set; }
        public virtual ICollection<Product> UpdatedProduct { get; set; }
        public virtual ICollection<Product> DeletedProduct { get; set; }
    }
}
