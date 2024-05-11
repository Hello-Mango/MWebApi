using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFireApi.Core
{
    public class ErrorResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Successed { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 调试错误信息
        /// </summary>
        public string DebugMessage { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; } = DateTime.UtcNow.Ticks.ToString();
    }
}
