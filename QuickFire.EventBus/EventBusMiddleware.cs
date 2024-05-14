using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.EventBus
{
    public static class EventBusMiddleware
    {
        public static IServiceCollection AddQuickFireEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus, EventBus>();
            return services;
        }

        public static IApplicationBuilder UseQuickFireEventBus(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
