using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using QuickFire.Infrastructure;
using QuickFire.Utils;
using System;
using System.Reflection;

namespace QuickFire.EFData
{
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
                        auditableEntity.ModificationTime = TimeUtils.GetTimeStamp();
                        auditableEntity.ModifiedAt = DateTimeOffset.UtcNow;
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
                    entry.Entity.ModificationTime = TimeUtils.GetTimeStamp();
                    entry.Entity.ModifierStaffId = userContext.UserId;
                    entry.Entity.ModifierStaffName = userContext.UserName;
                    entry.Entity.ModifiedAt = DateTimeOffset.UtcNow; 
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreationTime = TimeUtils.GetTimeStamp();
                    entry.Entity.CreatorStaffId = userContext.UserId;
                    entry.Entity.CreatorStaffName = userContext.UserName;
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow; ;
                }
            }
        }
    }

}
