using System;
using System.Collections.Generic;
using System.Text;

namespace MWebApi.Core
{
    /// <summary>
    /// cache interface
    /// </summary>
    public interface ICacheService
    {
        public string Get(string key);

        public T Get<T>(string key);

        public bool Remove(string key);

        public bool Set<T>(T t);

        public bool Set(string body);
    }
}
