using QuickFire.Core;
using QuickFire.BizException;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
using QuickFire.Infrastructure;
using System.Net;

namespace QuickFire.Infrastructure.Middlewares
{
    /// <summary>
    /// 全局异常捕获中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;  // 用来处理上下文请求
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IStringLocalizer _stringLocalizer;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IStringLocalizer stringLocalizer)
        {
            _next = next;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
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
                Successed = false
            };
            switch (ex)
            {
                case Exception422 ex422:
                    if (ex422.Params != null)
                    {
                        try
                        {
                            errorResponse.Message = string.Format(ex422.Message, ex422.Params);
                        }
                        catch (Exception ex2)
                        {
                            errorResponse.Message = $"Exception result formatting error {ex422.Message}";
                            errorResponse.DebugMessage = ex2.Message + ex2.StackTrace;
                        }
                    }
                    else
                    {
                        errorResponse.Message = ex422.Message;
                    }
                    response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
                    break;
                case EnumException bizEx:
                    if (bizEx.Params != null)
                    {
                        try
                        {
                            errorResponse.Message = string.Format(_stringLocalizer[bizEx.Code.ToString()], bizEx.Params);
                        }
                        catch (Exception ex2)
                        {
                            errorResponse.Message = $"Exception result formatting error {bizEx.Message}";
                            errorResponse.DebugMessage = ex2.Message + ex2.StackTrace;
                        }
                    }
                    else
                    {
                        errorResponse.Message = _stringLocalizer[bizEx.Code.ToString()];
                    }
                    response.StatusCode = (int)HttpStatusCode.UnprocessableContent;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(ex.Message + ex.StackTrace);
                    errorResponse.DebugMessage = ex.StackTrace ?? ex.Message;
                    break;
            }
            await context.Response.WriteAsJsonAsync(errorResponse);
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
