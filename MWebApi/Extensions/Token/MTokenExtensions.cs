using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MWebApi.Extensions.SwaggerExtensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

namespace MWebApi.Extensions.Token
{
    public static class MTokenExtension
    {
        public static IServiceCollection AddMToken(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton(new MTokenHandler(configuration));
            return service;
        }
        public static IServiceCollection AddMAuth(this IServiceCollection service, IConfigurationSection section)
        {
            //AddMToken(service, section);
            string secretKey = section.GetValue<string>("SecretKey");
            var secretByte = Encoding.UTF8.GetBytes(secretKey);
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //配置token验证
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = section.GetValue<string>("Issuer"),
                    ValidateAudience = true,
                    ValidAudience = section.GetValue<string>("Audience"),
                    //验证是否过期
                    ValidateLifetime = true,
                    //验证私钥
                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)

                };
            });
            return service;
        }
    }
}
