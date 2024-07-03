using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuickFireApi.Extensions.SwaggerExtensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

namespace QuickFireApi.Extensions.Token
{
    public static class MTokenExtension
    {
        public static IServiceCollection AddMToken(this IServiceCollection service, IConfiguration configuration, Func<TokenValidatedContext, Task>? func = null)
        {
            service.AddSingleton(new MTokenHandler(configuration));
            return service;
        }
        public static IServiceCollection AddMAuth(this IServiceCollection service, string secretKey, string issuer, string audience, Func<TokenValidatedContext, Task>? func = null)
        {
            var secretByte = Encoding.UTF8.GetBytes(secretKey);
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //配置token验证
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    //验证是否过期
                    ValidateLifetime = true,
                    //验证私钥
                    IssuerSigningKey = new SymmetricSecurityKey(secretByte),
                };
                if (func != null)
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = func
                    };
                }
                else
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            await Task.CompletedTask;
                        }
                    };
                }

            });
            return service;
        }
    }
}
