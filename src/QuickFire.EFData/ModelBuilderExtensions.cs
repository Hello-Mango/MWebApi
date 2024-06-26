using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.EFData
{
    public static class ModelBuilderExtensions
    {
        public static void AddSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeleted).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filter = Expression.Lambda(Expression.Equal(
                        Expression.Property(parameter, nameof(ISoftDeleted.Deleted)),
                        Expression.Constant(false)
                    ), parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }
        public static void AddTenantQueryFilter(this ModelBuilder modelBuilder,UserContext userContext)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ITenant).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filter = Expression.Lambda(Expression.Equal(
                        Expression.Property(parameter, nameof(ITenant.TenantId)),
                        Expression.Constant(userContext.TenantId)
                    ), parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }
        public static void RegisterAllEntities(this ModelBuilder modelBuilder)
        {
            var entityTypes = Assembly.Load("QuickFire.Domain.Entity").GetTypes()
                .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(BaseEntity)))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType);
            }
        }
    }
}
