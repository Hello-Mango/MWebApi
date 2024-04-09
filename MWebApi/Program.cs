
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MMemoryCache;
using MRedisCache;
using MWebApi.Core;
using MWebApi.Extensions;
using MWebApi.Extensions.Cache;
using MWebApi.Extensions.JsonExtensions;
using MWebApi.Extensions.Snowflake;
using MWebApi.Extensions.SwaggerExtensions;
using MWebApi.Extensions.Token;
using Serilog;
using Serilog.Events;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace MWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddJsonLocalization(z => z.ResourcesPath = "i18n");
            builder.Services.AddSwagger(true);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //MyPolicy Ϊ�Զ���Ĳ������ƣ���ʹ��ʱ��ͬ���ɡ�����ͬʱ��������ͬ�������ƵĿ������
            });

            builder.Services.AddMToken(configuration);
            builder.Services.AddSnowflake(configuration);
            var section = configuration.GetSection("JWTConfig");
            builder.Services.AddMAuth(section);

            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, AuthorizePolicyProvider>());
            builder.Services.AddMemoryCache();
            builder.Services.AddCacheService<MMemoryCacheService, MMemoryCacheOptions>(c =>
            {
                c.CacheKeyPrefix = "MWebApi";
            });
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtension();
            }
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            app.UseCors("MyPolicy");
            app.UseAuthorization();
            app.MapControllers().RequireAuthorization();

            app.Run();
        }
    }
}
