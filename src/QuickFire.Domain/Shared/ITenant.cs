using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Domain.Shared
{
    public interface ITenant
    {
        long TenantId { get; }
    }
}
