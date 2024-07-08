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
    public class TenantConfigManager : ITenantConfigManager
    {
        public static IDictionary<long, TenantConfigDictionary> _tenantDics = new ConcurrentDictionary<long, TenantConfigDictionary>();
        private readonly IServiceProvider _serviceProvider;
        private readonly IGenerateId<long> _generateId;
        public TenantConfigManager(IServiceProvider serviceProvider, IGenerateId<long> generateId)
        {
            _serviceProvider = serviceProvider;
            _generateId = generateId;
        }
        public string? GetTenantConfig(string key)
        {
            var sessionContext = _serviceProvider.GetRequiredService<ISessionContext>();

            bool flag1 = _tenantDics.TryGetValue(sessionContext.TenantId, out TenantConfigDictionary? tenantConfig);
            if (flag1 == false)
            {
                LoadByTenant(sessionContext.TenantId);
                tenantConfig = _tenantDics[sessionContext.TenantId];
            }
            if (tenantConfig!.isLoad)
            {
                LoadByTenant(sessionContext.TenantId);
            }
            bool flag = tenantConfig.TryGetValue(key, out string? result);
            return result;
        }

        public void SetTenantConfig(string key, string value)
        {
            var sessionContext = _serviceProvider.GetRequiredService<ISessionContext>();
            bool flag1 = _tenantDics.TryGetValue(sessionContext.TenantId, out TenantConfigDictionary? tenantConfig);
            if (flag1 == false)
            {
                LoadByTenant(sessionContext.TenantId);
                tenantConfig = _tenantDics[sessionContext.TenantId];
            }
            if (tenantConfig!.isLoad)
            {
                LoadByTenant(sessionContext.TenantId);
            }
            if (tenantConfig.ContainsKey(key))
            {
                var db = _serviceProvider.GetRequiredService<ApplicationDbContext>();
                var item = db.Set<TSysConfig>().FirstOrDefault(x => x.ConfigKey == key && x.TenantId == sessionContext.TenantId);
                if (item != null)
                {
                    item.ConfigValue = value;
                    db.Update(item);
                    db.SaveChanges();
                }
                tenantConfig[key] = value;
            }
            else
            {
                var db = _serviceProvider.GetRequiredService<ApplicationDbContext>();
                db.Add(new TSysConfig() { Id = _generateId.NextId(), ConfigKey = key, ConfigValue = value, TenantId = sessionContext.TenantId });
                db.SaveChanges();
                tenantConfig.TryAdd(key, value);
            }


            /*
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
             */
        }

        private void LoadByTenant(long tenantId)
        {
            TenantConfigDictionary tenantConfig = new TenantConfigDictionary()
            {
                TenantId = tenantId
            };
            var db = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var items = db.Set<TSysConfig>().Where(z => z.TenantId == tenantId);
            foreach (var item in items)
            {
                tenantConfig.TryAdd(item.ConfigKey, item.ConfigValue);
            }
            if (_tenantDics.ContainsKey(tenantId))
            {
                _tenantDics[tenantId] = tenantConfig;
            }
            else
            {
                _tenantDics.TryAdd(tenantId, tenantConfig);
            }
            tenantConfig.isLoad = true;
        }
    }
    public class TenantConfigDictionary : ConcurrentDictionary<string, string?>
    {
        public bool isLoad = false;
        public long TenantId { get; set; }
    }

}
