using Microsoft.EntityFrameworkCore;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Entites
{
    public class TSysConfig : BaseEntity, ITenant<long>
    {
        [Comment("配置键")]
        [MaxLength(30)]
        public string ConfigKey { get; set; }
        [Comment("配置值")]
        [MaxLength(100)]
        public string ConfigValue { get; set; }
        [Comment("备注")]
        [MaxLength(300)]
        public string Remark { get; set; }
        [Comment("租户ID")]
        public long TenantId { get; set;}

        [Comment("备用字段1")]
        [MaxLength(50)]
        public string Field1 { get; set; }

        [Comment("备用字段2")]
        [MaxLength(50)]
        public string Field2 { get; set; }

        [Comment("备用字段3")]
        [MaxLength(50)]
        public string Field3 { get; set; }
    }
}
