using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuickFire.Infrastructure;
using QuickFire.Infrastructure.DataSourceRoute;
using ShardingCore;
using System.Data;

namespace QuickFireApi.Extensions
{
    public static class ShardingCoreExtension
    {
        public static IServiceCollection AddShardingCore(this IServiceCollection service, string connectionString, string dbType)
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
                        switch (dbType.ToLower())
                        {
                            case "sqlserver":
                                builder.UseSqlServer(connectionString);
                                break;
                            case "mysql":
                                builder.UseMySQL(connectionString);
                                break;
                            case "pgsql":
                                builder.UseNpgsql(connectionString);
                                break;
                            default:
                                throw new Exception("Invalid database type");
                        }
                    });
                    op.UseShardingTransaction((connection, builder) =>
                    {
                        //connection is delegate input param
                        switch (dbType.ToLower())
                        {
                            case "sqlserver":
                                builder.UseSqlServer(connection);
                                break;
                            case "mysql":
                                builder.UseMySQL(connection);
                                break;
                            case "pgsql":
                                builder.UseNpgsql(connection);
                                break;
                            default:
                                throw new Exception("Invalid database type");
                        }
                    });
                    //use your data base connection string
                    op.AddDefaultDataSource(Guid.NewGuid().ToString("n"), connectionString);
                }).AddShardingCore();
            return service;
        }
    }
}
