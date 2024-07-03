
using QuickFire.Extensions.Core;

namespace QuickFireApi.Filters
{
    public class ApiLogging : IApiLogging
    {
        private readonly ILogger<ApiLogging> _logger;

        public ApiLogging(ILogger<ApiLogging> logger)
        {
            _logger = logger;
        }
        public Task AddLog(ApiLoggingModel context)
        {
            _logger.LogInformation("Api Logging: {context}", context);
            return Task.CompletedTask;
        }
    }
}
