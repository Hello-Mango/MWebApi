using MCoreInterface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace MRedisCache
{
    public class MRedisCacheService : ICacheService
    {
        private static ConnectionMultiplexer redisConnection { get; set; }
        private readonly MRedisCacheOptions _options;
        private readonly ILogger<MRedisCacheService> _logger;
        public MRedisCacheService(IOptions<MRedisCacheOptions> options, ILogger<MRedisCacheService> logger)
        {
            _logger = logger;
            _options = options.Value;
            redisConnection = ConnectionMultiplexer.Connect(_options.ConnectionString);
            redisConnection.InternalError += (sender, e) =>
            {
                _logger.LogError(e.Exception, "Redis Internal Error");
            };
            redisConnection.ErrorMessage += (sender, e) =>
            {
                _logger.LogError(e.Message, "Redis Error");
            };
            redisConnection.ConnectionFailed += (sender, e) =>
            {
                _logger.LogError(e.Exception, "Redis Connection Failed");
            };
            redisConnection.ConnectionRestored += (sender, e) =>
            {
                _logger.LogWarning("Redis Connection Restored");
            };
        }
        public string Get(string key)
        {
            var res = redisConnection.GetDatabase().StringGet(key);
            return res;
        }

        public T Get<T>(string key)
        {
            var stringRes = redisConnection.GetDatabase().StringGet(key);
            if (string.IsNullOrEmpty(stringRes))
            {
                return default;
            }
            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringRes);
            return res;
        }

        public Task<string> GetAsync(string key)
        {
            var res = redisConnection.GetDatabase().StringGetAsync(key).ContinueWith(t =>
            {
                return t.Result.ToString();
            });
            return res;
        }

        public Task<T> GetAsync<T>(string key)
        {
            var res = redisConnection.GetDatabase().StringGetAsync(key).ContinueWith(t =>
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(t.Result);
            });
            return res;
        }

        public bool Remove(string key)
        {
            return redisConnection.GetDatabase().KeyDelete(key);
        }

        public Task<bool> RemoveAsync(string key)
        {
            return redisConnection.GetDatabase().KeyDeleteAsync(key);
        }


        public bool Set<T>(string key, T t, int absoluteExpirationRelativeToNow)
        {
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            return redisConnection.GetDatabase().StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(t), timespan);
        }

        public bool Set(string key, string body, int absoluteExpirationRelativeToNow)
        {
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            return redisConnection.GetDatabase().StringSet(key, body, timespan);
        }

        public Task<bool> SetAsync<T>(string key, T t, int absoluteExpirationRelativeToNow)
        {
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            return redisConnection.GetDatabase().StringSetAsync(key, Newtonsoft.Json.JsonConvert.SerializeObject(t), timespan);
        }

        public Task<bool> SetAsync(string key, string body, int absoluteExpirationRelativeToNow)
        {
            var timespan = TimeSpan.FromSeconds(absoluteExpirationRelativeToNow);
            return redisConnection.GetDatabase().StringSetAsync(key, body, timespan);
        }

        public bool Set<T>(string key, T t)
        {
            return redisConnection.GetDatabase().StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(t));
        }

        public bool Set(string key, string body)
        {
            return redisConnection.GetDatabase().StringSet(key, body);
        }

        public Task<bool> SetAsync<T>(string key, T t)
        {
            return redisConnection.GetDatabase().StringSetAsync(key, Newtonsoft.Json.JsonConvert.SerializeObject(t));
        }

        public Task<bool> SetAsync(string key, string body)
        {
            return redisConnection.GetDatabase().StringSetAsync(key, body);
        }
    }
}
