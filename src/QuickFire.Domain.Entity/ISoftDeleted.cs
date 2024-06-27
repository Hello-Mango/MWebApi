using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Domain.Entity
{
    public interface ISoftDeleted
    {
        bool Deleted { get; set; }
    }
}
