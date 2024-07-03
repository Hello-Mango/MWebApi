using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickFire.Extensions.Core;

public class ApiMonitorActionFilter : IAsyncActionFilter
{
    private readonly ILogger<ApiLoggingActionFilter> _logger;

    private readonly IApiMonitor _apiMonitor;
    public ApiMonitorActionFilter(ILogger<ApiLoggingActionFilter> logger, IApiMonitor apiMonitor)
    {
        _logger = logger;
        _apiMonitor = apiMonitor;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 记录请求开始的时间
        var stopwatch = Stopwatch.StartNew();
        // 获取请求内容
        var request = context.HttpContext.Request;
        //request.EnableBuffering();
        //var reader = new StreamReader(request.Body);
        //var requestBody = await reader.ReadToEndAsync();
        //request.Body.Position = 0; // 重置流的位置
        // 获取User-Agent和请求地址
        var requestPath = request.GetDisplayUrl();
        // 执行Action
        var executedContext = await next();
        // 停止计时器
        stopwatch.Stop();

        await _apiMonitor.Monitor(new ApiMonitorModel()
        {
            HttpMethod = request.Method,
            Url = requestPath,
            TimeTick = stopwatch.ElapsedMilliseconds,
        });
    }
}