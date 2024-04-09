using MCoreInterface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MMemoryCache
{
    public class MMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MMemoryCacheOptions _mMemoryCacheOptions;
        public MMemoryCacheService(IMemoryCache memoryCache, IOptions<MMemoryCacheOptions> mMemoryCacheOptions)
        {
            _memoryCache = memoryCache;
            _mMemoryCacheOptions = mMemoryCacheOptions.Value;
        }
        public string? Get(string key)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            return _memoryCache.Get<string>(key);
        }

        public T Get<T>(string key)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            return _memoryCache.Get<T>(key);
        }

        public async Task<string?> GetAsync(string key)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            return await Task.FromResult(_memoryCache.Get<string>(key));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            return await Task.FromResult(_memoryCache.Get<T>(key));
        }

        public bool Remove(string key)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            _memoryCache.Remove(key);
            return true;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            await Task.Run(() => _memoryCache.Remove(key));
            return true;
        }

        public bool Set<T>(string key, T t, int absoluteExpirationRelativeToNow)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            _memoryCache.Set(key, t, timespan);
            return true;
        }

        public bool Set(string key, string body, int absoluteExpirationRelativeToNow)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            _memoryCache.Set(key, body, timespan);
            return true;
        }

        public async Task<bool> SetAsync<T>(string key, T t, int absoluteExpirationRelativeToNow)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            await Task.Run(() => _memoryCache.Set(key, t, timespan));
            return true;
        }

        public async Task<bool> SetAsync(string key, string body, int absoluteExpirationRelativeToNow)
        {
            key = _mMemoryCacheOptions.CacheKeyPrefix + key;
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            await Task.Run(() => _memoryCache.Set(key, body, timespan));
            return true;
        }
    }
}
