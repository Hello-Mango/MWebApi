using QuickFireApi.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using QuickFire.Core;
using System.Diagnostics.CodeAnalysis;
using QuickFire.Extensions.Core;
using QuickFire.Extensions.UserContext;
using QuickFire.Utils;
using System.Security.Claims;

namespace QuickFireApi.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (context.User.Identity is not null)
            {
                if (context.User.Identity!.IsAuthenticated)
                {
                    var userContext = serviceProvider.GetRequiredService<IUserContext>();
                    long.TryParse(context.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value, out long userId);
                    long.TryParse(context.User.Claims.FirstOrDefault(x => x.Type == "TenantId")?.Value, out long tenantId);
                    var roles = context.User.Claims
                                .Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value)
                                .ToList();
                    userContext.SetUserContext(userId, context.User.Identity.Name!, tenantId, roles);
                }
            }

            await _next(context!);
        }
    }
    public static class UserContextBuilderExtensions
    {
        public static IApplicationBuilder UseUserContextMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserContextMiddleware>();
        }
    }
    public static class UserContextExtension
    {
        public static IServiceCollection AddUserContext(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            return services;
        }
    }
}