using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EFCore
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);

        public PagedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
            {
                return query.Where(predicate);
            }
            return query;
        }
        public static IQueryable<T> WhereBetween<T, TProp>(
       this IQueryable<T> query,
       Expression<Func<T, TProp>> propertyExpression,
       TProp lowerBound,
       TProp upperBound)
       where T : class
        {
            var parameter = propertyExpression.Parameters.Single();
            var property = (PropertyInfo)((MemberExpression)propertyExpression.Body).Member;
            var lowerBoundExpression = Expression.GreaterThanOrEqual(
                propertyExpression.Body,
                Expression.Constant(lowerBound, typeof(TProp)));
            var upperBoundExpression = Expression.LessThanOrEqual(
                propertyExpression.Body,
                Expression.Constant(upperBound, typeof(TProp)));
            var andExpression = Expression.AndAlso(lowerBoundExpression, upperBoundExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(andExpression, parameter);

            return query.Where(lambda);
        }
        public static PagedList<T> ToPageList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            // 计算总记录数
            var totalCount = source.Count();

            // 分页查询
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            // 创建并返回PagedList实例
            return new PagedList<T>(items, totalCount, pageIndex, pageSize);
        }
    }
}
