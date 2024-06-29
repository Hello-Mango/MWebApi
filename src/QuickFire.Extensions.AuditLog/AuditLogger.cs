using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.AuditLog
{
    public class AuditLogger : IAuditLogger
    {
        private readonly ILogger _logger;
        public AuditLogger(ILogger logger)
        {
            _logger = logger;
        }
        public void Log(AuditLog message)
        {
            _logger.LogInformation(message.ToString());
        }

        public Task LogAsync(AuditLog message)
        {
            return Task.Run(() => Log(message));
        }
    }
}
