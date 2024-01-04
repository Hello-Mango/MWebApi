
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MWebApi.Extensions.SwaggerExtensions;
using Serilog;
using Serilog.Events;
using System.Text;

namespace MWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var builderC = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            IConfiguration configuration = builderC.Build();

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
                options.AddPolicy("MyPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //MyPolicy Ϊ�Զ���Ĳ������ƣ���ʹ��ʱ��ͬ���ɡ�����ͬʱ��������ͬ�������ƵĿ������
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var section = configuration.GetSection("JWTConfig");
                string secretKey = section.GetValue<string>("SecretKey");
                var secretByte = Encoding.UTF8.GetBytes(secretKey);
                //����token��֤
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = section.GetValue<string>("Issuer"),
                    ValidateAudience = true,
                    ValidAudience = section.GetValue<string>("Audience"),
                    //��֤�Ƿ����
                    ValidateLifetime = true,
                    //��֤˽Կ
                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)

                };
            });

            builder.Services.AddSingleton(new Extensions.Token.MTokenHandler(configuration));

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
