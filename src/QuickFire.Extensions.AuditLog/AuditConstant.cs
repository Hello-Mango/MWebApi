using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.AuditLog
{
    public class AuditConstant
    {
        public string EventId { get; set; }

        public static object EntityChanges { get; set; }
    }
}
