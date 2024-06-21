using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Core
{
    public interface ISoftDeleted
    {
        bool Deleted { get; set; }
    }
}
