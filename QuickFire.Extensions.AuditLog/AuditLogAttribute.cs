using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.AuditLog
{
    public class AuditLogAttribute : ActionFilterAttribute
    {
        public string EventType { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sp = context.HttpContext.RequestServices;
            var ctxItems = context.HttpContext.Items;

            try
            {
                // 在操作执行前
                var executedContext = await next();

                // 在操作执行后

                // 获取当前用户的身份信息
                //var user = await authService.GetUserFromJwt(executedContext.HttpContext.User);

                // 构造AuditLog对象
                var auditLog = new AuditLog
                {
                    EventId = Guid.NewGuid().ToString(),
                    EventType = this.EventType,
                    UserId = "user.UserId",
                    Username = "user.Username",
                    Timestamp = DateTime.UtcNow,
                    IPAddress = GetIpAddress(executedContext.HttpContext),
                    Description = $"操作类型：{this.EventType}",
                };

                if (ctxItems.TryGetValue(AuditConstant.EntityChanges, out var item))
                {
                    auditLog.EntityChanges = item as List<EntityChangeInfo>;
                }

                var routeData = new Dictionary<string, object?>();
                foreach (var (key, value) in context.RouteData.Values)
                {
                    routeData.Add(key, value);
                }

                auditLog.RouteData = routeData;

                var auditService = sp.GetRequiredService<IAuditLogService>();
                await auditService.LogAsync(auditLog);
            }
            catch (Exception ex)
            {
                var logger = sp.GetRequiredService<ILogger<AuditLogAttribute>>();
                logger.LogError(ex, "An error occurred while logging audit information.");
            }

            Console.WriteLine(
              "执行 AuditLogAttribute, " +
              $"EventId: {ctxItems["AuditLog_EventId"]}");
        }

        private string? GetIpAddress(HttpContext httpContext)
        {
            // 首先检查X-Forwarded-For头（当应用部署在代理后面时）
            var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {
                return forwardedFor.Split(',').FirstOrDefault(); // 可能包含多个IP地址
            }

            // 如果没有X-Forwarded-For头，或者需要直接获取连接的远程IP地址
            return httpContext.Connection.RemoteIpAddress?.ToString();
        }
    }

}
