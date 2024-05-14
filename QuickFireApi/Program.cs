using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QucikFire.Extensions;
using QuickFire.MemoryCache;
using QuickFireApi.Core;
using QuickFireApi.Extensions;
using QuickFireApi.Extensions.Addons;
using QuickFireApi.Extensions.Cache;
using QuickFireApi.Extensions.JsonExtensions;
using QuickFireApi.Extensions.Snowflake;
using QuickFireApi.Extensions.SwaggerExtensions;
using QuickFireApi.Extensions.Token;
using QuickFireApi.Middlewares;
using QuickFireApi.SignalR;
using Serilog;
using System.Globalization;
using QuickFire.Extensions.Quartz;
using Microsoft.Extensions.FileProviders;
using QuickFire.EventBus;

namespace QuickFireApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;
            builder.Services.AddSerilog();
            builder.Services.AddSerilog(logger =>
            {
                logger.ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext();
            });
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
            });
            builder.Services.AddAddons();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddJsonLocalization(z => z.ResourcesPath = "i18n");
            builder.Services.AddSwagger(true);
            builder.Services.AddSingleton<IUserPermission, UserPermission>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //MyPolicy Ϊ�Զ���Ĳ������ƣ���ʹ��ʱ��ͬ���ɡ�����ͬʱ��������ͬ�������ƵĿ������
            });
            builder.Services.AddHealthChecks();
            builder.Services.AddQuickFireEventBus(configuration);
            builder.Services.AddSnowflake(configuration);
            builder.Services.AddMToken(configuration);
            var section = configuration.GetSection("JWTConfig");
            builder.Services.AddMAuth(section);
            builder.Services.AddUserContext();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IAuthorizationHandler, MAuthorizationHandler>();
            builder.Services.AddCacheService<MemoryCacheService, QMemoryCacheOptions>(c =>
            {
                c.CacheKeyPrefix = "QuickFireApi";
            });
            builder.Services.AddSignalR(z =>
            {
            });
            builder.Services.AddQuickFireQuartz(configuration);
            var app = builder.Build();
            if (configuration.GetSection("Swagger").GetValue<bool>("IsShow"))
            {
                app.UseSwaggerExtension();
            }
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN"),
            };
            app.UseStaticFiles();
            app.UseQuickFireQuartzUI();
            app.UseAddonsUI();
            app.UseHealthChecks("/health");
            app.UseSerilogRequestLogging();
            app.UseExceptionMiddleware();
            app.UseUserContextMiddleware();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            app.UseCors("MyPolicy");
            app.UseAuthorization();
            app.MapControllers().RequireAuthorization();
            app.MapHub<ChatHub>("/Chat");
            app.Run();
        }
    }
}
