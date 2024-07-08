using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Filters
{
    public class CusTransactionFilter<T> : Attribute, IAsyncActionFilter where T : DbContext
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            var _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork<T>>();
            //await _unitOfWork.BeginTransactionAsync();
            //var result = next();
            //if (result.Exception == null)
            //{
            //    await _unitOfWork.CommitTransactionAsync();
            //}
            //else
            //{
            //    await _unitOfWork.RollbackTransactionAsync();
            //}
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                var result = await next();
                if (result.Exception == null)
                {
                    await _unitOfWork.CommitTransactionAsync();
                }
                return;
            }
        }
    }
}
