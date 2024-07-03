using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Domain.Entity.Base
{
    public interface ISoftDeleted
    {
        bool Deleted { get; set; }
    }
}
