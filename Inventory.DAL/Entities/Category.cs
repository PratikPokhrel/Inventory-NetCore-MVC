using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Entities
{
   public class Category:AuditableEntity
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }
        public string Name { get; set; }

        public virtual ICollection<Product> Product  { get; set; }
    }
}
