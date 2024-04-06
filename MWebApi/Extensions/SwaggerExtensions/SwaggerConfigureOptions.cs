using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;

namespace MWebApi.Extensions.SwaggerExtensions
{
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiDescriptionGroupCollectionProvider provider;

        public SwaggerConfigureOptions(IApiDescriptionGroupCollectionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiDescriptionGroups.Items)
            {
                if (string.IsNullOrEmpty(description.GroupName) == false)
                {
                    options.SwaggerGeneratorOptions.SwaggerDocs.Add(description.GroupName, new OpenApiInfo()
                    {
                        Description = description.GroupName,
                        Title = description.GroupName,
                    });
                }
            }
        }
    }
}
