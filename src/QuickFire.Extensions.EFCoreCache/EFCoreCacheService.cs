using QucikFire.Extensions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EFCoreCache
{
    public class EfCoreCacheService : ICacheService
    {
        private readonly CacheDbContext _dbContext;

        public EfCoreCacheService(CacheDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> GetAsync(string key)
        {
            var cacheItem = await _dbContext.CacheItems.FindAsync(key);
            return cacheItem?.Value;
        }

        public string? Get(string key)
        {
            var cacheItem = _dbContext.CacheItems.Find(key);
            return cacheItem?.Value;
        }

        public T? Get<T>(string key) where T : class
        {
            var cacheItem = _dbContext.CacheItems.Find(key);
            return cacheItem != null ? JsonSerializer.Deserialize<T>(cacheItem.Value) : null;
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            var cacheItem = await _dbContext.CacheItems.FindAsync(key);
            return cacheItem != null ? JsonSerializer.Deserialize<T>(cacheItem.Value) : null;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            var cacheItem = await _dbContext.CacheItems.FindAsync(key);
            if (cacheItem != null)
            {
                _dbContext.CacheItems.Remove(cacheItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public bool Remove(string key)
        {
            var cacheItem = _dbContext.CacheItems.Find(key);
            if (cacheItem != null)
            {
                _dbContext.CacheItems.Remove(cacheItem);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Set<T>(string key, T t, int absoluteExpirationRelativeToNow)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T t, int absoluteExpirationRelativeToNow)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, string body, int absoluteExpirationRelativeToNow)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string body, int absoluteExpirationRelativeToNow)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T t)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, string body)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string body)
        {
            throw new NotImplementedException();
        }
    }
}
