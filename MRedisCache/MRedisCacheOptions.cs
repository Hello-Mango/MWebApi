using System;
using System.Collections.Generic;
using System.Text;

namespace MRedisCache
{
    public class MRedisCacheOptions
    {
        public string ConnectionString { get; set; }

        public string CachePrefix { get; set; }
    }
}
