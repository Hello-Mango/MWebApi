using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.DbContexts
{
    public static class EntityEntryExtensions
    {
        public static EntityEntry SetColumnIf<TEntity, TProperty>(
            this EntityEntry<TEntity> entry, bool condition,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            TProperty value) where TEntity : class
        {
            if (condition)
            {
                var property = entry.Property(propertyExpression);
                property.CurrentValue = value;
                property.IsModified = true;
            }
            return entry;
        }
        /// <summary>
        ///  entry.SetColumnIf((x => x.TotalAmount, 200.00M), order.TotalAmount > 100);
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entry"></param>
        /// <param name="condition"></param>
        /// <param name="propertyValueTuple"></param>
        /// <returns></returns>
        public static EntityEntry SetColumnIf<TEntity, TProperty>(
       this EntityEntry<TEntity> entry, bool condition,
       (Expression<Func<TEntity, TProperty>> PropertyExpression, TProperty Value) propertyValueTuple
      ) where TEntity : class
        {
            if (condition)
            {
                var property = entry.Property(propertyValueTuple.PropertyExpression);
                property.CurrentValue = propertyValueTuple.Value;
                property.IsModified = true;
            }
            return entry;
        }
        /// <summary>
     //   entry.SetColumnsIf(
     //condition: order.TotalAmount > 100,
     //propertyValuePairs: new (Expression<Func<Order, object>>, object)[]
     //{
     //    (x => x.TotalAmount, 200.00M),
     //    (x => x.Status, "Processed")
     //});
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entry"></param>
        /// <param name="condition"></param>
        /// <param name="propertyValuePairs"></param>
        public static void SetColumnsIf<TEntity>(
       this EntityEntry<TEntity> entry,
       bool condition,
       params (Expression<Func<TEntity, object>> PropertyExpression, object Value)[] propertyValuePairs) where TEntity : class
        {
            if (condition)
            {
                foreach (var pair in propertyValuePairs)
                {
                    var property = entry.Property(pair.PropertyExpression);
                    property.CurrentValue = pair.Value;
                    property.IsModified = true;
                }
            }
        }
    }
}
