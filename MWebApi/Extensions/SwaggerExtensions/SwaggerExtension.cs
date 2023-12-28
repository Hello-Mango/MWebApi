using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.RegularExpressions;

namespace MWebApi.Extensions.SwaggerExtensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection service, List<SwaggerGroup> groupList, bool isAuth = false)
        {
            service.AddSwaggerGen(c =>
            {
                groupList.ForEach(group => c.SwaggerDoc(group.Name, group.ToOpenApiInfo()));
                // 添加文档信息
                var path = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");  // xml文档绝对路径
                c.IncludeXmlComments(path, true); // true : 显示控制器层注释
                c.OrderActionsBy(o => o.RelativePath); // 对action的名称进行排序，如果有多个，就可以看见效果了。
                if (isAuth)
                {
                    // 添加swagger Header验证
                    var scheme = new OpenApiSecurityScheme()
                    {
                        Description = "Authorization header. \r\nExample: 'Bearer token'",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Authorization"
                        },
                        Scheme = "oauth2",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    };
                    c.AddSecurityDefinition("Authorization", scheme);
                    var requirement = new OpenApiSecurityRequirement();
                    requirement[scheme] = new List<string>();
                    c.AddSecurityRequirement(requirement);
                }
            });
            return service;
        }

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app, List<SwaggerGroup> groupList, Action<SwaggerOptions> setupAction = null, Action<SwaggerUIOptions> setupUIAction = null)
        {
            if (setupAction != null)
            {
                app.UseSwagger(setupAction);
            }
            else
            {
                app.UseSwagger();
            }
            if (setupAction != null)
            {
                app.UseSwaggerUI(setupUIAction);
            }
            else
            {
                app.UseSwaggerUI(c =>
                {
                    c.EnablePersistAuthorization();
                    groupList.ForEach(group => c.SwaggerEndpoint($"/swagger/{group.Name}/swagger.json", group.Name));
                });
            }

            return app;
        }
    }
}
