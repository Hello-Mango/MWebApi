using QuickFire.Core;
using System;

namespace QuickFire.Core
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>, ISoftDeleted
    {
        public string CreatorStaffName { get; set; }
        public long CreatorStaffId { get; set; }

        public DateTime CreationTime { get; set; }

        public long CreatedAt { get; set; }

        public string ModifierStaffName { get; set; }

        public long ModifierStaffId { get; set; }

        public DateTime ModificationTime { get; set; }

        public long ModifiedAt { get; set; }
        public string DeletedStaffName { get; set; }
        public DateTime DeletedTime { get; set; }
        public bool Deleted { get; set; }
    }
}
