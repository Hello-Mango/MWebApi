using System;
using System.Collections.Generic;
using System.Text;
using QuickFire.Core;
using QuickFire.Domain.Entity.Base;

namespace QQuickFire.Domain.Entity
{
    public class SysApp : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    
        public string Logo { get; set; }

        public string VisitUrl { get; set; }
        public bool IsOperation { get; set; }

    }
}
