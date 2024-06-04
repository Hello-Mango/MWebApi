using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QuickFireApi.Extensions.SwaggerExtensions
{
    public class LongToStringSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(long))
            {
                schema.Type = "string";
            }
        }
    }
}
