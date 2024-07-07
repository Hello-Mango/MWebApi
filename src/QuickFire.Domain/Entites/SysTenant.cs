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
    public class SysTenant : BaseEntity
    {
        [Comment("租户编号")]
        [StringLength(50)]
        public string No { get; set; }
        [Comment("租户名称")]
        [StringLength(50)]
        public string Name { get; set; }

        [Comment("租户描述")]
        [StringLength(200)]
        public string Description { get; set; }

        [Comment("租户管理员")]

        public long UserId { get; set; }

        [Comment("租户类型")]
        [StringLength(50)]
        public string SchemaType { get; set; }

        [Comment("数据库连接字符串Id")]
        public long ConnectionStringId { get; set; }

        [Comment("数据库类型")]
        [StringLength(50)]
        public string DbType { get; set; }
    }
}
