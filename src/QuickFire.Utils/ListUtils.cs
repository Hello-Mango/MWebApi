﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickFire.Utils
{
    public static class ListUtils
    {
        public static ConcurrentDictionary<string, object> CacheDictionary = new ConcurrentDictionary<string, object>();
        /// <summary>
        /// 构建一个object数据转换成一维数组数据的委托
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="propertyInfos"></param>
        /// <returns></returns>
        public static Func<T, object[]> BuildObjectGetValuesDelegate<T>(List<PropertyInfo> propertyInfos) where T : class
        {
            var objParameter = Expression.Parameter(typeof(T), "model");
            var selectExpressions = propertyInfos.Select(it => BuildObjectGetValueExpression(objParameter, it));
            var arrayExpression = Expression.NewArrayInit(typeof(object), selectExpressions);
            var result = Expression.Lambda<Func<T, object[]>>(arrayExpression, objParameter).Compile();
            return result;
        }

        /// <summary>
        /// 构建对象获取单个值得
        /// </summary>
        /// <param name="modelExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static Expression BuildObjectGetValueExpression(ParameterExpression modelExpression, PropertyInfo propertyInfo)
        {
            var propertyExpression = Expression.Property(modelExpression, propertyInfo);
            var convertExpression = Expression.Convert(propertyExpression, typeof(object));
            return convertExpression;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> source, List<PropertyInfo>? propertyInfos = null, bool useColumnAttribute = false) where T : class
        {
            var table = new DataTable("template");
            if (propertyInfos == null || propertyInfos.Count == 0)
            {
                propertyInfos = typeof(T).GetProperties().Where(it => it.CanRead).ToList();
            }
            foreach (var propertyInfo in propertyInfos)
            {
                var columnName = useColumnAttribute ? (propertyInfo.GetCustomAttribute<ColumnAttribute>()?.Name ?? propertyInfo.Name) : propertyInfo.Name;
                table.Columns.Add(columnName, ChangeType(propertyInfo.PropertyType));
            }

            Func<T, object[]> func;
            var key = typeof(T).FullName + string.Join("_", propertyInfos.Select(it => it.Name).ToList());
            if (CacheDictionary.TryGetValue(key, out var cacheFunc))
            {
                func = (Func<T, object[]>)cacheFunc;
            }
            else
            {
                func = BuildObjectGetValuesDelegate<T>(propertyInfos);
                CacheDictionary.TryAdd(key, func);
            }

            foreach (var model in source)
            {
                var rowData = func(model);
                table.Rows.Add(rowData);
            }

            return table;
        }

        private static Type ChangeType(Type type)
        {
            if (type.IsNullable())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return type;
        }
        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
