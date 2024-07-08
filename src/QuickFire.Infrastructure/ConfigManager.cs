using Microsoft.EntityFrameworkCore;
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
        public static IDictionary<string, string> _config = new ConcurrentDictionary<string, string>();
        public bool isLoad = false;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGenerateId<long> _generateId;
        public ConfigManager(IServiceProvider serviceProvider, IGenerateId<long> generateId)
        {
            _serviceProvider = serviceProvider;
            _generateId = generateId;

        }
        public string? GetConfig(string key)
        {
            if (isLoad == false)
            {
                Load();
                isLoad = true;
            }
            bool flag1 = _config.TryGetValue(key, out string? result);
            return result;
        }

        public void SetConfig(string key, string value)
        {
            if (isLoad == false)
            {
                Load();
                isLoad = true;
            }
            if (_config.ContainsKey(key))
            {
                var db = _serviceProvider.GetRequiredService<ApplicationDbContext>();
                var item = db.Set<SysConfig>().FirstOrDefault(x => x.ConfigKey == key);
                if (item != null)
                {
                    item.ConfigValue = value;
                    db.Update(item);
                    db.SaveChanges();
                }
                _config[key] = value;
            }
            else
            {
                var db = _serviceProvider.GetRequiredService<ApplicationDbContext>();
                db.Add(new SysConfig() { Id = _generateId.NextId(), ConfigKey = key, ConfigValue = value });
                db.SaveChanges();
                _config.TryAdd(key, value);
            }
        }

        private void Load()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var items = db.Set<SysConfig>().ToList();
            foreach (var item in items)
            {
                _config.TryAdd(item.ConfigKey, item.ConfigValue);
            }
        }
    }
}
