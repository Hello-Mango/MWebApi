using Microsoft.EntityFrameworkCore;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Extensions
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
    }
}
