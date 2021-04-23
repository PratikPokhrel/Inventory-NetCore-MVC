using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DAL.Entities
{
    public class Product : AuditableEntity
    {
        public Guid? CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual Category Category  { get; set; }
    }
}
