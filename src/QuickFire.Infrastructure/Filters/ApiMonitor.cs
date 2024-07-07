
using Microsoft.Extensions.Logging;
using QuickFire.Extensions.Core;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure
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
