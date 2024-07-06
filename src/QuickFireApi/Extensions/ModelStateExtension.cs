using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using QuickFire.BizException;
using QuickFire.Core;

namespace QuickFireApi.Extension
{
    public static class ModelStateExtension
    {
        public static IServiceCollection AddModelState(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //获取验证失败的模型字段 
                    var errors = actionContext.ModelState
                        .Where(s => s.Value != null && s.Value.ValidationState == ModelValidationState.Invalid)
                        .SelectMany(s => s.Value!.Errors.ToList())
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var serviceProvider = actionContext.HttpContext.RequestServices;
                    var translateService = serviceProvider.GetService<IStringLocalizer>();
                    if (translateService != null)
                    {
                        errors = errors.Select(e => translateService.GetString(e).Value).ToList();
                    }
                    throw new Exception422(string.Join(Environment.NewLine, errors));
                };
            });
            return service;
        }
    }
}
