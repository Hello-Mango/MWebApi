
using IOTEdgeServer.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
using MWebApi.Middlewares;
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
                options.AddPolicy("MyPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //MyPolicy 为自定义的策略名称，与使用时相同即可。可以同时定义多个不同策略名称的跨域策略
            });

            builder.Services.AddMToken(configuration);
            builder.Services.AddSnowflake(configuration);
            var section = configuration.GetSection("JWTConfig");
            builder.Services.AddMAuth(section);
            builder.Services.AddUserContext();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, AuthorizePolicyProvider>());
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IAuthorizationHandler, MAuthorizationHandler>();
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
            app.UseExceptionMiddleware();
            app.UseUserContextMiddleware();
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
