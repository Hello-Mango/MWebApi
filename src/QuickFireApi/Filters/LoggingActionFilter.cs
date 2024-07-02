using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class LoggingActionFilter : IAsyncActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 记录请求开始的时间
        var stopwatch = Stopwatch.StartNew();

        // 获取请求内容
        var request = context.HttpContext.Request;
        request.EnableBuffering();
        var reader = new StreamReader(request.Body);
        var requestBody = await reader.ReadToEndAsync();
        request.Body.Position = 0; // 重置流的位置

        // 获取User-Agent和请求地址
        var userAgent = request.Headers["User-Agent"].ToString();
        var requestPath = request.GetDisplayUrl();

        // 执行Action
        var executedContext = await next();

        // 记录响应内容
        var objectResult = executedContext.Result as ObjectResult;
        var responseBody = objectResult?.Value;

        // 停止计时器
        stopwatch.Stop();

        // 构建日志信息
        var logMessage = new StringBuilder();
        logMessage.AppendLine($"Request Path: {requestPath}");
        logMessage.AppendLine($"Request Body: {requestBody}");
        logMessage.AppendLine($"User-Agent: {userAgent}");
        logMessage.AppendLine($"Response: {responseBody}");
        logMessage.AppendLine($"Duration: {stopwatch.ElapsedMilliseconds} ms");

        // 根据需要将日志记录到数据库或文本文件
        LogToStorage(logMessage.ToString());

        // 使用ILogger记录到控制台或日志文件
        _logger.LogInformation(logMessage.ToString());
    }

    private void LogToStorage(string logMessage)
    {
        // 这里实现具体的日志存储逻辑，比如写入数据库或文件
        // 示例仅为日志输出，实际应用中应根据需求实现具体逻辑
        Debug.WriteLine("Log to storage: " + logMessage);
    }
}