
using QucikFire.Extensions;

namespace QuickFireApi.Extensions.Cache
{
    public static class CacheExtension
    {
        public static IServiceCollection AddCacheService<T, TOption>(this IServiceCollection services, Action<TOption> setupAction)
            where T : class, ICacheService
            where TOption : class
        {
            services.Configure(setupAction);
            services.AddSingleton<ICacheService, T>();
            return services;
        }
    }
}
