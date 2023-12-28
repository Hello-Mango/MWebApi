using Microsoft.OpenApi.Models;

namespace MWebApi.Extensions.SwaggerExtensions
{
    public class SwaggerGroup
    {
        /// <summary>
        /// 分组名称（同时用于做URL前缀）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分组Title（显示的信息）
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 分组描述 
        /// </summary>
        public string? Description { get; set; }

        public SwaggerGroup(string name, string? title = null, string? description = null)
        {
            Name = name;
            Title = title;
            Description = description;
        }

        /// <summary>
        /// 生成 <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/>
        /// </summary>
        public OpenApiInfo ToOpenApiInfo(string version = "1.0")
        {
            return new OpenApiInfo { Title = Title, Description = Description, Version = version };
        }
    }
}
