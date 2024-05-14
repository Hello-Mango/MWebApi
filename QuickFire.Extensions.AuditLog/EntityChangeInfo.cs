using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.AuditLog
{
    public class EntityChangeInfo
    {
        public string Entity { get; set; }
        public string Action { get; set; }
        public string Sql { get; set; }
        public Dictionary<string, object?> Parameters { get; set; }
    }
}
