using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Shared
{
    public interface IConfigManager
    {
        public string? GetConfig(string key);
        public void SetConfig(string key, string value);
    }
}
