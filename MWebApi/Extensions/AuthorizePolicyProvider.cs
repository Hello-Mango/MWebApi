using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using MWebApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace MWebApi.Extensions
{
    public class AuthorizePolicyProvider : IApplicationModelProvider
    {
        public int Order => 1;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {

        }
        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                controller.Filters.Add(new AuthorizeFilter(controller.ControllerName));
            }
        }
    }
}
