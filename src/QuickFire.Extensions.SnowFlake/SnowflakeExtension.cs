
using Microsoft.Extensions.DependencyInjection;
using QuickFire.Extensions.Core;
using System;

namespace QuickFire.Extensions.Snowflake
{
    public static class SnowflakeExtension
    {
        public static IServiceCollection AddSnowflake(this IServiceCollection services, Action<SnowflakeOption> setupAction)
        {
            services.Configure(setupAction);
            services.AddSingleton<IGenerateId<long>, Snowflake>();
            services.AddSingleton<SnowflakeId>();
            return services;
        }

        
    }
}
