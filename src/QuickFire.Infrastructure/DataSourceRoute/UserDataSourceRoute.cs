using ShardingCore.Core.EntityMetadatas;
using ShardingCore.Core.VirtualRoutes.DataSourceRoutes.Abstractions;
using ShardingCore.Core.VirtualRoutes;
using ShardingCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickFire.Domain.Entity;
using ShardingCore.Core.VirtualRoutes.TableRoutes.Abstractions;
using ShardingCore.VirtualRoutes.Months;

namespace QuickFire.Infrastructure.DataSourceRoute
{
    public class UserDataSourceRoute : AbstractSimpleShardingMonthKeyDateTimeVirtualTableRoute<SysUser>
    {
        public override bool AutoCreateTableByTime()
        {
            return true;
        }

        public override void Configure(EntityMetadataTableBuilder<SysUser> builder)
        {
            builder.ShardingProperty(o => o.CreatedAt);
        }

        public override DateTime GetBeginTime()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }
    }
}
