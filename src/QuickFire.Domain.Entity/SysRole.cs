using QuickFire.Core;
using QuickFire.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Domain.Entity
{
    public class SysRole : BaseEntity
    {
        public string No { get; set; }

        public string Name { get; set; }

        public string TenantId { get; set; }

        public string Decs { get; set; }
    }
}
