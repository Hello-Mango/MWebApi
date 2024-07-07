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
        public IDictionary<long, TenantConfigDictionary> _tenantDics = new ConcurrentDictionary<long, TenantConfigDictionary>();
        private readonly IServiceProvider _serviceProvider;
        public TenantConfigManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
                tenantConfig[key] = value;
            }
            else
            {
                tenantConfig.TryAdd(key, value);
            }
        }

        private void LoadByTenant(long tenantId)
        {
            TenantConfigDictionary tenantConfig = new TenantConfigDictionary()
            {
                TenantId = tenantId
            };
            var db = _serviceProvider.GetRequiredService<SysDbContext>();
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
