using Microsoft.AspNetCore.Mvc.Filters;

namespace QuickFireApi.Filters
{
    public class TransactionFilterAttribute : IActionFilter
    {
        public TransactionFilterAttribute()
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
