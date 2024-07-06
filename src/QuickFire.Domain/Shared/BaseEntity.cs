using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace QuickFire.Domain.Shared
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
        [StringLength(50)]
        [Comment("创建人编号")]
        public string CreatorStaffNo { get; set; }
        [Comment("创建人ID")]
        public TId CreatorStaffId { get; set; }

        [Comment("创建时间")]
        public DateTimeOffset CreatedAt { get; set; }

        [StringLength(50)]
        [Comment("修改员工编号")]
        public string? ModifierStaffNo { get; set; }

        [Comment("修改员工ID")]
        public TId? ModifierStaffId { get; set; }
        [Comment("修改时间")]
        public DateTimeOffset? ModifiedAt { get; set; }

        [StringLength(50)]
        [Comment("删除员工编号")]
        public string? DeletedStaffNo { get; set; }
        [Comment("删除标记 0：否 1：是")]
        public bool Deleted { get; set; }

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
    public abstract class BaseEntityLId :IEntity<long>
    {
        [Key]
        public long Id { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<long>, ISoftDeleted, IEntity<long>
    {

    }
}
