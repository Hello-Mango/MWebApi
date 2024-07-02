using QuickFire.Extensions.ApiFilter;

namespace QuickFireApi.Filters
{
    public class ApiMonitor : IApiMonitor
    {
        private readonly ILogger<ApiMonitor> _logger;
        public ApiMonitor(ILogger<ApiMonitor> logger)
        {
            _logger = logger;
        }
        public Task Monitor(ApiMonitorModel context)
        {
            _logger.LogInformation("{Method} {Path} {StatusCode}", context.HttpMethod, context.Url, context.TimeTick);
            return Task.CompletedTask;
        }
    }
}
