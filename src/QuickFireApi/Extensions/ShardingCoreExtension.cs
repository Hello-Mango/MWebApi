using Microsoft.EntityFrameworkCore;
using QuickFire.Infrastructure;
using QuickFire.Infrastructure.DataSourceRoute;
using ShardingCore;

namespace QuickFireApi.Extensions
{
    public static class ShardingCoreExtension
    {
        public static IServiceCollection AddShardingCore(this IServiceCollection service)
        {
            //额外添加分片配置
            service.AddShardingDbContext<SysDbContext>()
                .UseRouteConfig(op =>
                {
                    op.AddShardingTableRoute<UserDataSourceRoute>();
                }).UseConfig(op =>
                {
                    op.UseShardingQuery((connStr, builder) =>
                    {
                        //connStr is delegate input param
                        builder.UseMySQL(connStr);
                    });
                    op.UseShardingTransaction((connection, builder) =>
                    {
                        //connection is delegate input param
                        builder.UseMySQL(connection);
                    });
                    //use your data base connection string
                    op.AddDefaultDataSource(Guid.NewGuid().ToString("n"), "Data Source=localhost;Initial Catalog=EFCoreShardingTableDB;Integrated Security=True;");
                }).AddShardingCore();
            return service;
        }
    }
}
