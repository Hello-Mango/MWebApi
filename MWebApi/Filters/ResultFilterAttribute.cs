using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MWebApi.Filters
{
    public class ResultFilterAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var result = context.Result as ObjectResult;
                context.Result = new JsonResult(new ApiResult<object> { StatusCode = 200, Succeeded = true, Data = result.Value });
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new ApiResult<object> { StatusCode = 200, Succeeded = true, Data = new { } });
            }
            else if (context.Result is ContentResult)
            {
                var result = context.Result as ContentResult;
                context.Result = new JsonResult(new ApiResult<string> { StatusCode = 200, Succeeded = true, Data = result.Content });
            }
            else
            {
                throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
            }
        }
    }
    public class ApiResult<T>
    {
        public int StatusCode { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 数据信息
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 成功标志
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// 当前时间戳
        /// </summary>
        public long Timestamp { get; set; } = DateTime.UtcNow.Ticks;
        /// <summary>
        /// 报错编号
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 报错内容
        /// </summary>
        public string Errors { get; set; }
    }
}