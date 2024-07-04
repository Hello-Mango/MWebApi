using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickFire.Extensions.Core;
using Newtonsoft.Json;

public class ApiLoggingActionFilter : IAsyncActionFilter
{
    private readonly ILogger<ApiLoggingActionFilter> _logger;

    private readonly IApiLogging _apiLogging;
    private readonly IUserContext _userContext;
    public ApiLoggingActionFilter(ILogger<ApiLoggingActionFilter> logger, IApiLogging apiLogging, IUserContext userContext)
    {
        _logger = logger;
        _apiLogging = apiLogging;
        _userContext = userContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 记录请求开始的时间
        var stopwatch = Stopwatch.StartNew();
        // 获取请求内容
        var request = context.HttpContext.Request;
        var requestBody = JsonConvert.SerializeObject(context.ActionArguments);
        //request.Body.Position = 0; // 重置流的位置

        // 获取User-Agent和请求地址
        var userAgent = request.Headers["User-Agent"].ToString();
        var client = QuickFire.Utils.UserAgent.UserAgent.GeSysUserAgent(userAgent);
        var requestPath = request.GetDisplayUrl();

        // 执行Action
        var executedContext = await next();

        // 记录响应内容
        var objectResult = executedContext.Result as ObjectResult;
        var responseBody = objectResult?.Value;

        // 停止计时器
        stopwatch.Stop();

        await _apiLogging.AddLog(new ApiLoggingModel()
        {
            Url = requestPath,
            TimeTick = stopwatch.ElapsedMilliseconds,
            BrowserName = client.Browser,
            IpAddress = _userContext.IpAddress,
            OSName = client.OS,
            UserName = _userContext.UserName,
            TenantName = _userContext.TenantId.ToString(),
            ReturnValue = JsonConvert.SerializeObject(responseBody),
            Param = requestBody
        });
    }
}