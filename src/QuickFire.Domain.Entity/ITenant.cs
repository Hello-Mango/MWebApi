using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Domain.Entity
{
    public interface ITenant
    {
        string TenantId { get; }
    }
}
