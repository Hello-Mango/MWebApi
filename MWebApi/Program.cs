
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MWebApi.Extensions.JsonExtensions;
using MWebApi.Extensions.SwaggerExtensions;
using MWebApi.Extensions.Token;
using Serilog;
using Serilog.Events;
using SqlSugar;
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
            List<SwaggerGroup> swaggerGroups = new List<SwaggerGroup>()
            {
                new SwaggerGroup("TEST1","TEST1 TITLE","TEST1 DESC"),
                new SwaggerGroup("Hello","Hello1 TITLE","Hello1 DESC"),
            };
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwagger(swaggerGroups, true);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //MyPolicy 为自定义的策略名称，与使用时相同即可。可以同时定义多个不同策略名称的跨域策略
            });
            builder.Services.AddScoped(s =>
            {
                ISqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.Sqlite,
                    ConnectionString = "DataSource=mwebapi.db",
                    IsAutoCloseConnection = true,
                });
                return sqlSugar;
            });
            builder.Services.AddMToken(configuration);
            var section = configuration.GetSection("JWTConfig");
            builder.Services.AddMAuth(section);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtension(swaggerGroups);
                var item = app.Services.GetRequiredService<ISqlSugarClient>();
                item.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Log.Information(sql + "\r\n" + item.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                };
            }
            app.UseCors("MyPolicy");
            app.UseAuthorization();
            app.MapControllers().RequireAuthorization();

            app.Run();
        }
    }
}
