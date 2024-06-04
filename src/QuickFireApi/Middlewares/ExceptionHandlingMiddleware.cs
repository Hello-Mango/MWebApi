using QuickFireApi.Core;
using Newtonsoft.Json;
using System.Net;

namespace QuickFireApi.Middlewares
{
    /// <summary>
    /// 全局异常捕获中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;  // 用来处理上下文请求
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); //要么在中间件中处理，要么被传递到下一个中间件中去
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex); // 捕获异常了 在HandleExceptionAsync中处理
            }
        }

        /// <summary>
        /// 异步处理异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            var errorResponse = new ErrorResponse  // 自定义的异常错误信息类型
            {
                Message = ex.Message,
                DebugMessage = ex.StackTrace ?? ex.Message,
                Successed = false
            };
            switch (ex)
            {
                case Exception422 ex422:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(ex.Message + ex.StackTrace);
                    break;
            }
            var result = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsJsonAsync(result);
        }
    }

    public static class ExceptionHandlingBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }

}
