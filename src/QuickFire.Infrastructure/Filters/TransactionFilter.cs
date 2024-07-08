using Microsoft.AspNetCore.Mvc.Filters;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Filters
{
    public class TransactionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var actionAttributes = context.ActionDescriptor.EndpointMetadata.OfType<ApiLoggingActionFilter>();
            if (actionAttributes.Count() > 0)
            {
                var result = next();
                return result;
            }
            else
            {
                _unitOfWork.BeginTransaction();
                var result = next();
                if (result.Exception == null)
                {
                    _unitOfWork.CommitTransaction();
                }
                else
                {
                    _unitOfWork.RollbackTransaction();
                }
                return result;
            }
        }
    }

    public class TransactionDisabled : Attribute
    { }

}
