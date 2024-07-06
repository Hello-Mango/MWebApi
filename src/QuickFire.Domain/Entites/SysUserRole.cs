using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using QuickFire.Domain.Shared;

namespace QuickFire.Domain.Entites
{
    public class SysUserRole : BaseEntityLId, ITenant
    {
        [Comment("用户Id")]
        public long UserId { get; set; }
        [Comment("角色Id")]
        public long RoleId { get; set; }

        [Comment("租户Id")]
        public long TenantId { get; set; }
    }
}
