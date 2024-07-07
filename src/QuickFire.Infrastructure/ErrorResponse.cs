
using System;

namespace QuickFire.Infrastructure
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
        public string Code { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; } = DateTimeOffset.UtcNow.Ticks.ToString();
    }
}
