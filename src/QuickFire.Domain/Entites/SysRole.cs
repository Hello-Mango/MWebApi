using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuickFire.Domain.Entites
{
    public class SysRole : BaseEntity, ITenant, IEntityLong
    {
        [Comment("角色编号")]
        [StringLength(32)]
        public string No { get; set; }
        [Comment("角色名称")]
        [StringLength(50)]
        public string Name { get; set; }
        [Comment("租户编号")]
        public long TenantId { get; set; }
        [Comment("角色描述")]
        [StringLength(200)]
        public string Decs { get; set; }
    }
}
