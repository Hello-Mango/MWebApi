using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Quartz
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举 文本值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <returns>文本字符串</returns>
        public static string ToText<TEnum>(this TEnum source) where TEnum : struct
        {
            Type type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException("非枚举类型不能调用ToText()方法", source.ToString());

            string? name = source.ToString();

            FieldInfo? field = type.GetField(name!);
            if (field != null)
            {
                object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes.Length > 0)
                {
                    string text = ((DescriptionAttribute)customAttributes[0]).Description;
                    return text;
                }
            }

            return string.Empty;
        }
    }
}
