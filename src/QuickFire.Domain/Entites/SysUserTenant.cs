using Microsoft.EntityFrameworkCore;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Entites
{
    public class SysUserTenant : BaseEntityLId
    {
        [Comment("用户Id")]
        public long UserId { get; set; }
        [Comment("租户Id")]
        public long TenantId { get; set; }
    }
}
