using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.AuditLog
{
    public interface IAuditLogService
    {
        Task LogAsync(AuditLog auditLog);
    }
}
