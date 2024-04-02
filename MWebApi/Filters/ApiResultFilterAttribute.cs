using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MWebApi.Core;

namespace MWebApi.Filters
{
    public class ApiResultFilterAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var result = context.Result as ObjectResult;
                context.Result = new JsonResult(new ApiResult<object> { statusCode = 200, succeeded = true, data = result.Value });
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new ApiResult<object> { statusCode = 200, succeeded = true, data = new { } });
            }
            else if (context.Result is ContentResult)
            {
                var result = context.Result as ContentResult;
                context.Result = new JsonResult(new ApiResult<string> { statusCode = 200, succeeded = true, data = result.Content });
            }
            else
            {
                throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
            }
        }
    }
    
}
