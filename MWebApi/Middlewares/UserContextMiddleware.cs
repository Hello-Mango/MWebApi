﻿using IOTEdgeServer.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using MWebApi.Core;
using MWebApi.Extensions.Snowflake;
using System.Diagnostics.CodeAnalysis;

namespace MWebApi.Middlewares
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
            if (context is not null)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var userContext = serviceProvider.GetRequiredService<UserContext>();
                    // Do something with userContext
                    userContext.UserName = context.User.Identity.Name;
                }
            }

            await _next(context);
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
            services.AddScoped<UserContext>();
            return services;
        }
    }
}