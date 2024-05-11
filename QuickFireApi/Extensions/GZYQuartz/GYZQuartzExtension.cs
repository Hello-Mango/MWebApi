using GZY.Quartz.MUI.EFContext;
using GZY.Quartz.MUI.Extensions;
using Microsoft.EntityFrameworkCore;

namespace QuickFireApi.Extensions.GZYQuartz
{
    public static class GYZQuartzExtension
    {
        public static WebApplicationBuilder AddGYZQuartz(this WebApplicationBuilder builder)
        {
            builder.WebHost.UseStaticWebAssets();
            return builder;
        }

        public static IServiceCollection AddGYZQuartz(this IServiceCollection service,DbContextOptions<QuarzEFContext> options)
        {
            service.AddQuartzUI(options); //注入UI组件
            service.AddQuartzClassJobs(); //注入任务
            return service;
        }

        public static IApplicationBuilder UseGYZQuartz(this IApplicationBuilder app)
        {
            app.UseQuartz(); //使用UI组件
            return app;
        }

    }
}
