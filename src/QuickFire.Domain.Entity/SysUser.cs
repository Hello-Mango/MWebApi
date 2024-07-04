using System;
using System.Collections.Generic;
using System.Text;
using QuickFire.Core;
using QuickFire.Domain.Entity.Base;

namespace QuickFire.Domain.Entity
{
    public class SysUser : BaseEntity
    {
        public string No { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

        public bool IsLock { get; set; }
    }
}
