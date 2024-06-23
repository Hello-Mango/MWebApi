using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace QuickFire.Utils
{
    /// <summary>
    /// 类型转换扩展
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        public static string SafeString(this object input)
        {
            return input?.ToString()?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="obj">数据</param>
        public static bool ToBool(this string obj)
        {
            return Convert.ToBool(obj);
        }

        /// <summary>
        /// 转换为可空bool
        /// </summary>
        /// <param name="obj">数据</param>
        public static bool? ToBoolOrNull(this string obj)
        {
            return Convert.ToBoolOrNull(obj);
        }

        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="obj">数据</param>
        public static int ToInt(this string obj)
        {
            return Convert.ToInt(obj);
        }

        /// <summary>
        /// 转换为可空int
        /// </summary>
        /// <param name="obj">数据</param>
        public static int? ToIntOrNull(this string obj)
        {
            return Convert.ToIntOrNull(obj);
        }

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="obj">数据</param>
        public static long ToLong(this string obj)
        {
            return Convert.ToLong(obj);
        }

        /// <summary>
        /// 转换为可空long
        /// </summary>
        /// <param name="obj">数据</param>
        public static long? ToLongOrNull(this string obj)
        {
            return Convert.ToLongOrNull(obj);
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="obj">数据</param>
        public static double ToDouble(this string obj)
        {
            return Convert.ToDouble(obj);
        }

        /// <summary>
        /// 转换为可空double
        /// </summary>
        /// <param name="obj">数据</param>
        public static double? ToDoubleOrNull(this string obj)
        {
            return Convert.ToDoubleOrNull(obj);
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="obj">数据</param>
        public static decimal ToDecimal(this string obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为可空decimal
        /// </summary>
        /// <param name="obj">数据</param>
        public static decimal? ToDecimalOrNull(this string obj)
        {
            return Convert.ToDecimalOrNull(obj);
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="obj">数据</param>
        public static DateTime ToDateTime(this string obj)
        {
            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="obj">数据</param>
        public static DateTime? ToDateTimeOrNull(this string obj)
        {
            return Convert.ToDateTimeOrNull(obj);
        }

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="obj">数据</param>
        public static Guid ToGuid(this string obj)
        {
            return Convert.ToGuid(obj);
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="obj">数据</param>
        public static Guid? ToGuidOrNull(this string obj)
        {
            return Convert.ToGuidOrNull(obj);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="obj">字符串集合</param>
        public static List<Guid> ToGuidList(this IList<string> obj)
        {
            if (obj == null)
                return new List<Guid>();
            return obj.Select(t => t.ToGuid()).ToList();
        }
    }

    public static class Convert
    {

        #region ToInt(转换为32位整型)

        /// <summary>
        /// 转换为32位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static int ToInt(object input)
        {
            return ToIntOrNull(input) ?? 0;
        }

        #endregion

        #region ToIntOrNull(转换为32位可空整型)

        /// <summary>
        /// 转换为32位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static int? ToIntOrNull(object input)
        {
            var success = int.TryParse(input.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = ToDoubleOrNull(input, 0);
                if (temp == null)
                    return null;
                return System.Convert.ToInt32(temp);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region ToLong(转换为64位整型)

        /// <summary>
        /// 转换为64位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long ToLong(object input)
        {
            return ToLongOrNull(input) ?? 0;
        }

        #endregion

        #region ToLongOrNull(转换为64位可空整型)

        /// <summary>
        /// 转换为64位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long? ToLongOrNull(object input)
        {
            var success = long.TryParse(input.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = ToDecimalOrNull(input, 0);
                if (temp == null)
                    return null;
                return System.Convert.ToInt64(temp);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region ToFloat(转换为32位浮点型)

        /// <summary>
        /// 转换为32位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float ToFloat(object input, int? digits = null)
        {
            return ToFloatOrNull(input, digits) ?? 0;
        }

        #endregion

        #region ToFloatOrNull(转换为32位可空浮点型)

        /// <summary>
        /// 转换为32位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float? ToFloatOrNull(object input, int? digits = null)
        {
            var success = float.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return (float)Math.Round(result, digits.Value);
        }

        #endregion

        #region ToDouble(转换为64位浮点型)

        /// <summary>
        /// 转换为64位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(object input, int? digits = null)
        {
            return ToDoubleOrNull(input, digits) ?? 0;
        }

        #endregion

        #region ToDoubleOrNull(转换为64位可空浮点型)

        /// <summary>
        /// 转换为64位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(object input, int? digits = null)
        {
            var success = double.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }

        #endregion

        #region ToDecimal(转换为128位浮点型)

        /// <summary>
        /// 转换为128位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(object input, int? digits = null)
        {
            return ToDecimalOrNull(input, digits) ?? 0;
        }

        #endregion

        #region ToDecimalOrNull(转换为128位可空浮点型)

        /// <summary>
        /// 转换为128位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(object input, int? digits = null)
        {
            var success = decimal.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }

        #endregion

        #region ToBool(转换为布尔值)

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool ToBool(object input)
        {
            return ToBoolOrNull(input) ?? false;
        }

        #endregion

        #region ToBoolOrNull(转换为可空布尔值)

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool? ToBoolOrNull(object input)
        {
            var value = input.SafeString();
            switch (value)
            {
                case "1":
                    return true;
                case "0":
                    return false;
            }
            bool.TryParse(value, out var result);
            return  result;
        }

        #endregion

        #region ToDateTime(转换为日期)

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime ToDateTime(object input)
        {
            return ToDateTimeOrNull(input) ?? DateTime.MinValue;
        }

        #endregion

        #region ToDateTimeOrNull(转换为可空日期)

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime? ToDateTimeOrNull(object input)
        {
            var success = DateTime.TryParse(input.SafeString(), out var result);
            if (success == false)
                return null;
            return result;
        }

        #endregion

        #region ToGuid(转换为Guid)

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static Guid ToGuid(object input)
        {
            return ToGuidOrNull(input) ?? Guid.Empty;
        }

        #endregion

        #region ToGuidOrNull(转换为可空Guid)

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static Guid? ToGuidOrNull(object input)
        {
            if (input == null)
                return null;
            if (input.GetType() == typeof(byte[]))
                return new Guid((byte[])input);
            Guid.TryParse(input.SafeString(), out var result);
            return result;
        }

        #endregion

        #region ToBytes(转换为字节数组)

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>        
        public static byte[] ToBytes(string input)
        {
            return ToBytes(input, Encoding.UTF8);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(string input, Encoding encoding)
        {
            return string.IsNullOrWhiteSpace(input) ? new byte[] { } : encoding.GetBytes(input);
        }

        #endregion

        #region ToList(泛型集合转换)

        #endregion
        #region ToDictionary(对象转换为属性名值对)

        /// <summary>
        /// 对象转换为属性名值对
        /// </summary>
        /// <param name="data">对象</param>
        public static IDictionary<string, object> ToDictionary(object data)
        {
            return ToDictionary(data, false);
        }

        /// <summary>
        /// 对象转换为属性名值对
        /// </summary>
        /// <param name="data">对象</param>
        /// <param name="useDisplayName">是否使用显示名称,可使用[Description] 或 [DisplayName]特性设置</param>
        public static IDictionary<string, object> ToDictionary(object data, bool useDisplayName)
        {
            var result = new Dictionary<string, object>();
            if (data == null)
                return result;
            if (data is IEnumerable<KeyValuePair<string, object>> dic)
                return new Dictionary<string, object>(dic);
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data))
            {
                var value = property.GetValue(data);
                result.Add(GetPropertyDescriptorName(property, useDisplayName), value);
            }
            return result;
        }

        /// <summary>
        /// 获取属性名
        /// </summary>
        private static string GetPropertyDescriptorName(PropertyDescriptor property, bool useDisplayName)
        {
            if (useDisplayName == false)
                return property.Name;
            if (string.IsNullOrEmpty(property.Description) == false)
                return property.Description;
            if (string.IsNullOrEmpty(property.DisplayName) == false)
                return property.DisplayName;
            return property.Name;
        }

        #endregion
    }

}
