using Microsoft.Extensions.DependencyInjection;
using QuickFire.Core;
using System.Diagnostics.CodeAnalysis;
using QuickFire.Extensions.Core;
using QuickFire.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using QuickFire.Extensions.sessionContext;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Middlewares
{
    public class SessionContextMiddleware
    {
        private readonly RequestDelegate _next;
        public SessionContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (context.User.Identity is not null)
            {
                if (context.User.Identity!.IsAuthenticated)
                {
                    var sessionContext = serviceProvider.GetRequiredService<ISessionContext>();
                    long.TryParse(context.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value, out long userId);
                    bool isHasTenant = context.Request.Headers.TryGetValue("X-Tenant-ID", out var tenantId);

                    string ipAddress = context.Connection.RemoteIpAddress!.ToString();
                    var roles = context.User.Claims
                                .Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value)
                                .ToList();
                    sessionContext.SetsessionContext(userId, context.User.Identity.Name!, roles);
                    if (isHasTenant)
                    {
                        sessionContext.SetTenant(long.Parse(tenantId!));
                    }
                }
            }
            await _next(context!);
        }
    }
    public static class SessionContextBuilderExtensions
    {
        public static IApplicationBuilder UsesessionContextMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SessionContextMiddleware>();
        }
    }
    public static class SessionContextExtension
    {
        public static IServiceCollection AddSessionContext(this IServiceCollection services)
        {
            services.AddScoped<ISessionContext, SessionContext>();
            return services;
        }
    }
}