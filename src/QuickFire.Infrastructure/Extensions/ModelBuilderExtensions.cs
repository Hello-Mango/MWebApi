using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickFire.Core;
using QuickFire.Domain.Shared;
using QuickFire.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
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
        public static void AddDateTimeConvert(this ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
           v => v, // 假设已经是 UTC，不进行转换
           v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // 从数据库读取时，指定为 UTC

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                }
            }
        }
        public static void AddDateTimeOffsetConvert(this ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTimeOffset, DateTimeOffset>(
           v => v.ToUniversalTime(), // 假设已经是 UTC，不进行转换
           v => v.ToLocalTime()); // 从数据库读取时，指定为 UTC

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTimeOffset) || property.ClrType == typeof(DateTimeOffset?))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                }
            }
        }
        public static void AddTenantQueryFilter(this ModelBuilder modelBuilder, ISessionContext sessionContext)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ITenant).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filter = Expression.Lambda(Expression.Equal(
                        Expression.Property(parameter, nameof(ITenant.TenantId)),
                        Expression.Constant(sessionContext.TenantId)
                    ), parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }
        public static void RegisterAllEntities(this ModelBuilder modelBuilder)
        {
            var entityTypes = Assembly.Load("QuickFire.Domain").GetTypes()
                .Where(t => !t.IsAbstract);
            var tt = typeof(IEntity<>);
            foreach (var entityType in entityTypes)
            {
                var types = FindRootInterfaces(entityType);
                if (types.Exists(z => z.Name == tt.Name))
                {
                    modelBuilder.Entity(entityType);
                }
            }
            //foreach (var entityType in entityTypes)
            //{
            //    modelBuilder.Entity(entityType);
            //}
        }
        public static List<Type> FindRootInterfaces(Type type)
        {
            List<Type> rootInterfaces = new List<Type>();
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsInterface && interfaceType.GetInterfaces().Length == 0)
                {
                    rootInterfaces.Add(interfaceType);
                }
                else
                {
                    // 如果接口还继承了其他接口，递归查找
                    rootInterfaces.AddRange(FindRootInterfaces(interfaceType));
                }
            }
            return rootInterfaces.Distinct().ToList(); // 去重，确保每个基础接口只出现一次
        }
    }
}
