
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Quartz;
using Quartz.AspNetCore;
using System.Reflection;

namespace QuickFire.Extensions.Quartz
{
    public static class QuickFireQuartz
    {
        public static IServiceCollection AddQuickFireQuartz(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection quartzConfiguration = configuration.GetSection("Quartz"); // Quartz配置节点
            if (quartzConfiguration == null)
            {
                throw new ArgumentNullException(nameof(quartzConfiguration));
            }
            services.AddHttpClient();
            services.AddQuartz(config =>
            {
                string dbType = quartzConfiguration["DbType"]!.ToLower();
                string tablePrefix = quartzConfiguration["TablePrefix"]!;
                string connectionString = quartzConfiguration["Database"]!;
                config.UseTimeZoneConverter();
                config.UseDefaultThreadPool(options =>
                {
                    options.MaxConcurrency = 30;    // 最大并发执行线程数
                });
                if (dbType == "memory")
                {
                    config.UseInMemoryStore();
                }
                else
                {
                    config.UsePersistentStore(options =>
                    {
                        options.UseNewtonsoftJsonSerializer();
                        options.UseProperties = false;
                        //options.UseNewtonsoftJsonSerializer();
                        if (dbType == "oracle")
                        {
                            options.UseOracle(ado =>
                            {
                                ado.ConnectionString = connectionString;
                                ado.TablePrefix = tablePrefix;  // 默认值 QRTZ_
                                ado.ConnectionStringName = "Quartz.net";
                            });
                        }
                        else if (dbType == "mysql")
                        {
                            options.UseMySql(ado =>
                            {
                                ado.ConnectionString = connectionString;
                                ado.TablePrefix = tablePrefix;  // 默认值 QRTZ_
                                ado.ConnectionStringName = "Quartz.net";
                            });
                        }
                        else if (dbType == "sqlserver")
                        {
                            options.UseSqlServer(ado =>
                            {
                                ado.ConnectionString = connectionString;
                                ado.TablePrefix = tablePrefix;  // 默认值 QRTZ_
                                ado.ConnectionStringName = "Quartz.net";
                            });
                        }
                        else if (dbType == "pgsql")
                        {
                            options.UsePostgres(ado =>
                            {
                                ado.ConnectionString = connectionString;
                                ado.TablePrefix = tablePrefix;  // 默认值 QRTZ_
                                ado.ConnectionStringName = "Quartz.net";
                            });
                        }
                    });
                }
                // 监听器
                config.AddSchedulerListener<DefaultSchedulerListener>();
                config.AddJobListener<DefaultJobListener>();
                config.AddTriggerListener<DefaultTriggerListener>();
            });

            //IHostedService宿主启动 Quartz服务 services.AddSingleton<IHostedService, QuartzHostedService>()
            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;   // 等待任务执行完，再退出
            });

            return services;
        }

        public static IApplicationBuilder UseQuickFireQuartzUI(this IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quartzui")),
                RequestPath = "/quartzui" // 访问文件的URL路径
            });
            return app;
        }
    }
}
