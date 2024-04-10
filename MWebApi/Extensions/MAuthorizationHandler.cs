using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace MWebApi.Extensions
{
    public class MAuthorizationHandler : IAuthorizationHandler
    {
        private readonly IServiceProvider _serviceProvider;
        public MAuthorizationHandler(IServiceProvider serviceProvider, IMemoryCache memoryCache)
        {
            _serviceProvider = serviceProvider;
        }
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User.IsInRole("admin"))
            {
                var requirement = context.Requirements.FirstOrDefault();
                context.Succeed(requirement);
            }
            else
            {
                var httpcontext = _serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
                if (httpcontext != null)
                {
                    var item = httpcontext.Request.Path;
                    if (context.User.IsInRole(item))
                    {

                    }
                }
                var requirement = context.Requirements.FirstOrDefault();
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
