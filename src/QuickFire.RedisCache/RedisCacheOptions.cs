using System;
using System.Collections.Generic;
using System.Text;

namespace QucikFire.Extensions
{
    public class RedisCacheOptions
    {
        public string ConnectionString { get; set; }

        public string CachePrefix { get; set; }
    }
}
