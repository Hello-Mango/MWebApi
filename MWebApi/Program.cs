
using Microsoft.OpenApi.Models;
using MWebApi.Extensions.SwaggerExtensions;

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
            builder.Services.AddSwagger(swaggerGroups);
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //MyPolicy 为自定义的策略名称，与使用时相同即可。可以同时定义多个不同策略名称的跨域策略
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtension(swaggerGroups);
            }
            app.UseCors("MyPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
