using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.ApiFilter
{
    public class ApiMonitorModel
    {
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public long TimeTick { get; set; }
        public DateTimeOffset MonitorTime { get; set; } = DateTimeOffset.UtcNow;
    }
}
