using Microsoft.Extensions.DependencyInjection;
using QuickFire.Domain.Entites;
using QuickFire.Domain.Shared;
using QuickFire.Extensions.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure
{
    public class ConfigManager : IConfigManager
    {
        public IDictionary<string, string> _config = new ConcurrentDictionary<string, string>();
        public bool isLoad = false;
        private readonly IServiceProvider _serviceProvider;
        public ConfigManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public string? GetConfig(string key)
        {
            if (isLoad == false)
            {
                Load();
            }
            bool flag1 = _config.TryGetValue(key, out string? result);
            return result;
        }

        public void SetConfig(string key, string value)
        {
            if (isLoad == false)
            {
                Load();
            }
            if (_config.ContainsKey(key))
            {
                _config[key] = value;
            }
            else
            {
                _config.TryAdd(key, value);
            }
        }

        private void Load()
        {
            var db = _serviceProvider.GetRequiredService<SysDbContext>();
            var items = db.Set<SysConfig>().ToList();
            foreach (var item in items)
            {
                _config.TryAdd(item.ConfigKey, item.ConfigValue);
            }
        }
    }
}
