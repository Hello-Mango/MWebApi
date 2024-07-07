using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace QuickFire.Domain.Shared
{
    public abstract class BaseEntity<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
    public abstract class BaseEntityLId : IEntity<long>
    {
        [Key]
        public long Id { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<long>, ISoftDeleted, IEntity<long>
    {
        public long Id { get; set; }
        [StringLength(50)]
        [Comment("创建人编号")]
        public string CreatorStaffNo { get; set; }
        [Comment("创建人ID")]
        public long CreatorStaffId { get; set; }

        [Comment("创建时间")]
        public DateTimeOffset CreatedAt { get; set; }

        [StringLength(50)]
        [Comment("修改员工编号")]
        public string? ModifierStaffNo { get; set; }

        [Comment("修改员工ID")]
        public long? ModifierStaffId { get; set; }
        [Comment("修改时间")]
        public DateTimeOffset? ModifiedAt { get; set; }

        [StringLength(50)]
        [Comment("删除员工编号")]
        public string? DeletedStaffNo { get; set; }
        [Comment("删除标记 0：否 1：是")]
        public bool Deleted { get; set; }
    }
    public abstract class BaseEntityString : BaseEntity<string>, ISoftDeleted, IEntity<string>
    {
        public string Id { get; set; }
        [StringLength(50)]
        [Comment("创建人编号")]
        public string CreatorStaffNo { get; set; }
        [Comment("创建人ID")]
        public string CreatorStaffId { get; set; }

        [Comment("创建时间")]
        public DateTimeOffset CreatedAt { get; set; }

        [StringLength(50)]
        [Comment("修改员工编号")]
        public string? ModifierStaffNo { get; set; }

        [Comment("修改员工ID")]
        public string? ModifierStaffId { get; set; }
        [Comment("修改时间")]
        public DateTimeOffset? ModifiedAt { get; set; }

        [StringLength(50)]
        [Comment("删除员工编号")]
        public string? DeletedStaffNo { get; set; }
        [Comment("删除标记 0：否 1：是")]
        public bool Deleted { get; set; }
    }
}
