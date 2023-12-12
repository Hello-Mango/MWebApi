using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MWebApi
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        IServiceCollection _services;
        public SwaggerDocumentFilter(IServiceCollection services)
        {
            _services = services;
        }
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.ActionDescriptor is ControllerActionDescriptor)
                {
                    ControllerActionDescriptor controllerActionDescriptor = (ControllerActionDescriptor)apiDescription.ActionDescriptor;
                    //if (!_services..Contains(controllerActionDescriptor.ControllerName))
                    //{
                    //    string key = "/" + controllerActionDescriptor.AttributeRouteInfo.Template;
                    //    swaggerDoc.Paths.Remove(key);
                    //}
                }
            }
        }
    }
}
