using MWebApi.Core;

namespace MWebApi.Extensions.Snowflake
{
    public static class MSnowflakeExtension
    {
        public static IServiceCollection AddSnowflake(this IServiceCollection services,long dcId,long workId)
        {
            services.AddSingleton<IGenerateId<long>, Snowflake>();
            services.AddSingleton(z =>
            {
                return new SnowflakeId(dcId, workId);
            });
            return services;
        }
        public static IServiceCollection AddSnowflake(this IServiceCollection services, IConfiguration configuration)
        {
            long dcId = configuration.GetValue<long>("Snowflake:DatacenterId");
            long workId = configuration.GetValue<long>("Snowflake:WorkerId");
            services.AddSingleton<IGenerateId<long>, Snowflake>();
            services.AddSingleton(z =>
            {
                return new SnowflakeId(dcId, workId);
            });
            return services;
        }
    }
}
