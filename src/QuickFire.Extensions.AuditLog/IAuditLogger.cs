using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.AuditLog
{
    public interface IAuditLogger
    {
        void Log(AuditLog message);

        Task LogAsync(AuditLog message);
    }
}
