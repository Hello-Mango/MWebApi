using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Filters
{
    public class TransactionFilter<T> : Attribute, IAsyncActionFilter where T : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        public TransactionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 检查请求方法是否为 GET，如果是，则直接执行下一个中间件
            if (context.HttpContext.Request.Method == HttpMethods.Get)
            {
                var result = await next();
                return;
            }
            var actionAttributes = context.ActionDescriptor.EndpointMetadata.OfType<TransactionDisabled>();
            if (actionAttributes.Count() > 0)
            {
                var result =await next();
                return;
            }
            else
            {
                var _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork<T>>();
                using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    var result =await next();
                    if (result.Exception == null)
                    {
                        await _unitOfWork.CommitTransactionAsync();
                    }
                    else
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                    }
                    return;
                }
            }
        }
    }

    public class TransactionDisabled : Attribute
    { }

}
