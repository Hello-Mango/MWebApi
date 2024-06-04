using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using QuickFireApi.Core;

namespace QuickFireApi.Extensions
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
    public class MAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IUserPermission _userPermission;
        private readonly IServiceProvider _serviceProvider;
        public MAuthorizationHandler(IServiceProvider serviceProvider, IUserPermission userPermission)
        {
            _serviceProvider = serviceProvider;
            _userPermission = userPermission;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User.IsInRole("admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
                bool checkResult = _userPermission.CheckPermission("", "");
                if (checkResult)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context is not null)
            {
                var descriptor = context?.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor is not null)
                {
                    var ctrlName = descriptor.ControllerName + "." + descriptor.ActionName;
                    var authorizationService = context!.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
                    var authorizationResult = await authorizationService!.AuthorizeAsync(context!.HttpContext.User, null, new PermissionAuthorizationRequirement(ctrlName));
                    if (!authorizationResult.Succeeded)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}
