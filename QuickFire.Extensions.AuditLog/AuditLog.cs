using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.AuditLog
{
    public class AuditLog
    {
        /// <summary>
        /// 事件唯一标识
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 事件类型（例如：登录、登出、数据修改等）
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 执行操作的用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 执行操作的用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 用户的IP地址
        /// </summary>
        public string? IPAddress { get; set; }

        /// <summary>
        /// 实体更改内容，可根据实际情况以JSON格式存储
        /// </summary>
        public List<EntityChangeInfo>? EntityChanges { get; set; } = new();

        /// <summary>
        /// 路由信息
        /// </summary>
        public Dictionary<string, object?> RouteData { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 额外信息 （考虑以 JSON 格式保存）
        /// </summary>
        public object? Extra { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; } = DateTime.UtcNow;
    }

}
