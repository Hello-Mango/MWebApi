using MWebApi.Core;
using Newtonsoft.Json;
using System.Net;

namespace IOTEdgeServer.Middlewares
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
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";  // 返回json 类型
            var response = context.Response;
            var errorResponse = new ApiResult<object>  // 自定义的异常错误信息类型
            {
                statusCode = 500,
                errors = exception.Message + exception.StackTrace
            };
            switch (exception)
            {
                case Exception422 ex:
                    string msg = ex.Message;
                    if (ex.InnerException != null)
                    {
                        msg += ex.InnerException.Message;
                    }
                    errorResponse.statusCode = (int)HttpStatusCode.UnprocessableContent;
                    response.StatusCode = errorResponse.statusCode;
                    errorResponse.errors = msg;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.errors = exception.Message + exception.StackTrace;
                    break;
            }
            _logger.LogError(exception.Message + exception.StackTrace);
            var result = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(result);
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
