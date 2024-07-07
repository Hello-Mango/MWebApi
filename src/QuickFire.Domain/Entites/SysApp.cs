using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using QuickFire.Domain.Shared;

namespace QQuickFire.Domain.Entites
{
    public class SysApp : BaseEntity
    {
        [Comment("应用编码")]
        [StringLength(50)]
        public string Code { get; set; }
        [Comment("应用名称")]
        [StringLength(50)]
        public string Name { get; set; }
        [Comment("应用描述")]
        [StringLength(500)]
        public string Description { get; set; }
        [Comment("应用图标")]
        [StringLength(200)]
        public string Logo { get; set; }
        [Comment("访问地址")]
        [StringLength(200)]
        public string VisitUrl { get; set; }
        [Comment("是否运营系统")]
        public bool IsOperation { get; set; }

    }
}
