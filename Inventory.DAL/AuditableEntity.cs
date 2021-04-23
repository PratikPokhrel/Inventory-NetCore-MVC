using Inventory.DAL.Entities;
using System;

namespace Inventory.DAL
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        public virtual ApplicationUser UpdatedByUser { get; set; }

        public virtual ApplicationUser DeletedByUser { get; set; }

    }
}
