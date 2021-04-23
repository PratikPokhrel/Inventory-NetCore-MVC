using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace Inventory.DAL.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public ApplicationUserRole() : base()
        { }
        public Guid Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }

    }
}
