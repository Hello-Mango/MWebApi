
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MWebApi.Extensions.SwaggerExtensions;
using System.Text;

namespace MWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            List<SwaggerGroup> swaggerGroups = new List<SwaggerGroup>()
            {
                new SwaggerGroup("TEST1","TEST1 TITLE","TEST1 DESC"),
                new SwaggerGroup("Hello","Hello1 TITLE","Hello1 DESC"),
            };

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger(swaggerGroups, true);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //MyPolicy 为自定义的策略名称，与使用时相同即可。可以同时定义多个不同策略名称的跨域策略
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //返回的是 字符的数组
                var secretByte = Encoding.UTF8.GetBytes(builder.Configuration["Auth:SecretKey"]);
                //配置token验证
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Auth:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Auth:Audience"],
                    //验证是否过期
                    ValidateLifetime = true,
                    //验证私钥
                    IssuerSigningKey = new SymmetricSecurityKey(sercrtByte)

                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtension(swaggerGroups);
            }
            app.UseCors("MyPolicy");
            app.UseAuthorization();


            app.MapControllers().RequireAuthorization();

            app.Run();
        }
    }
}
