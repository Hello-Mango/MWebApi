using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using QuickFire.Extensions.Interface;
using System.Runtime.Loader;

namespace QuickFireApi.Extensions.Addons
{
    public static class AddonsLoader
    {
        public static IServiceCollection AddAddons(this IServiceCollection services)
        {

            foreach (var dir in System.IO.Directory.GetDirectories(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Addons")))
            {
                Directory.GetFiles(dir, "*Api.dll", SearchOption.TopDirectoryOnly).ToList().ForEach(file =>
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                    bool isLoad = assembly.GetTypes().ToList().Exists(x => x.GetInterfaces().Contains(typeof(IAddon)));
                    if (isLoad)
                    {
                        services.AddControllers().AddApplicationPart(assembly);
                    }
                });
            }

            //Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Addons"), "*Api.dll", SearchOption.AllDirectories).ToList().ForEach(file =>
            //{
            //    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
            //    bool isLoad = assembly.GetTypes().ToList().Exists(x => x.GetInterfaces().Contains(typeof(IAddon)));
            //    if (isLoad)
            //    {
            //        services.AddControllers().AddApplicationPart(assembly);
            //    }
            //});
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
            return services;
        }

        public static IApplicationBuilder UseAddonsUI(this IApplicationBuilder app)
        {
            //遍历addons目录下的所有文件夹，取出文件夹下的wwwroot的目录，将其添加到静态文件中间件，二级目录为文件夹名
            Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Addons")).ToList().ForEach(dir =>
            {
                var wwwroot = Path.Combine(dir, "wwwroot");
                if (Directory.Exists(wwwroot))
                {
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(wwwroot),
                        RequestPath = "/" + new DirectoryInfo(dir).Name
                    });
                }
            });
            return app;
        }
    }
}
