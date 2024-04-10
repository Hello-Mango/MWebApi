using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MWebApi.Extensions.SwaggerExtensions
{

    //public class AllGroupDocumentFilter : IDocumentFilter
    //{
    //    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    //    {
    //        // Create a new OpenApiDocument that includes all APIs
    //        var allApisDocument = new OpenApiDocument
    //        {
    //            Paths = context.ApiDescriptions
    //             .GroupBy(api => api.RelativePath)
    //             .ToDictionary(
    //                 group => group.Key,
    //             ),
    //            Components = context.SchemaRepository.Components,
    //            Info = new OpenApiInfo { Title = "All APIs", Version = "v1" }
    //        };

    //        // Merge the allApisDocument into the "All" document
    //        foreach (var path in allApisDocument.Paths)
    //        {
    //            swaggerDoc.Paths.Add(path.Key, path.Value);
    //        }
    //    }
    //}

    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection service, bool isAuth = false)
        {
            service.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("All", new OpenApiInfo { Title = "All APIs", Version = "v1" });
                //c.DocumentFilter<AllGroupDocumentFilter>();
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (docName == "All")
                    {
                        return true;
                    }
                    return docName == apiDesc.GroupName;
                });
                // 添加文档信息
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPathMain = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPathMain, true); // true : 显示控制器层注释

                Assembly assembly = Assembly.GetExecutingAssembly();
                List<string> referencedAssemblies = assembly.GetReferencedAssemblies().Select(z => z.Name).ToList();
                DirectoryInfo d = new DirectoryInfo(AppContext.BaseDirectory);
                FileInfo[] files = d.GetFiles("*.xml");
                foreach (var xml in files)
                {
                    if (referencedAssemblies.Contains(xml.Name[..^4]))
                    {
                        c.IncludeXmlComments(xml.FullName, true); // true : 显示控制器层注释
                    }
                }

                c.OrderActionsBy(o => o.RelativePath); // 对action的名称进行排序，如果有多个，就可以看见效果了。
                if (isAuth)
                {
                    // 添加swagger Header验证
                    var scheme = new OpenApiSecurityScheme()
                    {
                        Description = "Authorization header. Example: 'Bearer token'",
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

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app, Action<SwaggerOptions>? setupAction = null, Action<SwaggerUIOptions> setupUIAction = null)
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
                       var apiDescriptionGroups = app.ApplicationServices.GetRequiredService<IApiDescriptionGroupCollectionProvider>().ApiDescriptionGroups.Items;
                       foreach (var description in apiDescriptionGroups)
                       {
                           if (string.IsNullOrEmpty(description.GroupName) == false)
                           {
                               c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                           }
                       }
                       c.SwaggerEndpoint($"/swagger/All/swagger.json", "All APIs");

                   });
            }

            return app;
        }
    }
}
