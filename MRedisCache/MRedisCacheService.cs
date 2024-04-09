using MCoreInterface;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MRedisCache
{
    public class MRedisCacheService : ICacheService
    {
        private readonly MRedisCacheOptions _options;
        public MRedisCacheService(IOptions<MRedisCacheOptions> options)
        {
            _options = options.Value;
            ///TODO: Add Redis Connection
        }
        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        
        public bool Set<T>(string key, T t)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, string body)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string body)
        {
            throw new NotImplementedException();
        }
    }
}
