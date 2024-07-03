using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Core
{
    public class ApiLoggingModel
    {
        public string TenantName { get; set; }
        public string UserName { get; set; }
        public string OSName { get; set; }
        public string BrowserName { get; set; }
        public string IpAddress { get; set; }
        public string Url { get; set; }
        public long TimeTick { get; set; }
        public DateTimeOffset LogTime { get; set; } = DateTimeOffset.UtcNow;

        public string ReturnValue { get; set; }

        public string Param { get; set; }
    }
}
