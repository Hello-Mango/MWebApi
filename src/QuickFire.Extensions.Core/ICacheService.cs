﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QucikFire.Extensions
{
    /// <summary>
    /// cache interface
    /// </summary>
    public interface ICacheService
    {
        public Task<string?> GetAsync(string key);
        public string? Get(string key);

        public T? Get<T>(string key) where T : class;
        public Task<T?> GetAsync<T>(string key) where T : class;

        public Task<bool> RemoveAsync(string key);
        public bool Remove(string key);

        public bool Set<T>(string key, T t, int absoluteExpirationRelativeToNow);
        public Task<bool> SetAsync<T>(string key, T t, int absoluteExpirationRelativeToNow);

        public bool Set(string key, string body, int absoluteExpirationRelativeToNow);
        public Task<bool> SetAsync(string key, string body, int absoluteExpirationRelativeToNow);

        public bool Set<T>(string key, T t);
        public Task<bool> SetAsync<T>(string key, T t);

        public bool Set(string key, string body);
        public Task<bool> SetAsync(string key, string body);
    }
}
