using System;
using System.Collections.Generic;
using System.Text;

namespace MRedisCache
{
    public class MRedisCacheOptions
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
