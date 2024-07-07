using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Shared
{
    public interface ITenantConfigManager
    {
        public string? GetTenantConfig(string key);
        public void SetTenantConfig(string key, string value);
    }
}
