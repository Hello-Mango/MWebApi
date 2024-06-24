using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using QuickFire.Utils;
using System;

namespace QuickFire.EFData
{
    // SoftDelete/DbContextExtensions.cs
    public static class DbContextExtensions
    {
        public static void HandleSoftDelete(this DbContext context, UserContext userContext)
        {
            foreach (var entry in context.ChangeTracker.Entries<ISoftDeleted>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted = true;
                    if (entry.Entity is BaseEntity auditableEntity)
                    {
                        auditableEntity.ModifierStaffId = userContext.UserId;
                        auditableEntity.ModifierStaffName = userContext.UserName;
                        auditableEntity.ModificationTime = DateTime.UtcNow;
                        auditableEntity.ModifiedAt = TimeUtils.GetTimeStamp();
                    }
                }
            }
        }

        public static void HandleAddModify(this DbContext context, UserContext userContext)
        {
            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModificationTime = DateTime.UtcNow;
                    entry.Entity.ModifierStaffId = userContext.UserId;
                    entry.Entity.ModifierStaffName = userContext.UserName;
                    entry.Entity.ModifiedAt = TimeUtils.GetTimeStamp();
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreationTime = DateTime.UtcNow;
                    entry.Entity.CreatorStaffId = userContext.UserId;
                    entry.Entity.CreatorStaffName = userContext.UserName;
                    entry.Entity.CreatedAt = TimeUtils.GetTimeStamp();
                }
            }
        }
    }

}
