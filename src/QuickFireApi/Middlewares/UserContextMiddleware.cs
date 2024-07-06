using QuickFireApi.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using QuickFire.Core;
using System.Diagnostics.CodeAnalysis;
using QuickFire.Extensions.Core;
using QuickFire.Utils;
using System.Security.Claims;
using QuickFire.Extensions.sessionContext;

namespace QuickFireApi.Middlewares
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
                    long.TryParse(context.User.Claims.FirstOrDefault(x => x.Type == "TenantId")?.Value, out long tenantId);
                    string ipAddress = context.Connection.RemoteIpAddress!.ToString();
                    var roles = context.User.Claims
                                .Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value)
                                .ToList();
                    sessionContext.SetsessionContext(userId, context.User.Identity.Name!, roles);
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
        public static IServiceCollection AddsessionContext(this IServiceCollection services)
        {
            services.AddScoped<ISessionContext, SessionContext>();
            return services;
        }
    }
}