using System;
using System.Security.Cryptography;

namespace QuickFire.Domain.Entity.Base
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        public string CreatorStaffName { get; set; }
        public TId CreatorStaffId { get; set; }

        public long CreationTime { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string? ModifierStaffName { get; set; }

        public TId? ModifierStaffId { get; set; }

        public long? ModificationTime { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>, ISoftDeleted
    {
        public string? DeletedStaffName { get; set; }
        public long? DeletedTime { get; set; }
        public bool Deleted { get; set; }
    }
}
