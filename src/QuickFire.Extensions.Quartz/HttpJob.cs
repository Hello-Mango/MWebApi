using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Quartz
{
    [PersistJobDataAfterExecution]  // 执行完，更新JobData
    [DisallowConcurrentExecution]   // 单个Job不允许并发 执行
    public class HttpJob : IJob
    {
        private readonly HttpClient _http;
        private readonly ILogger<HttpJob> _logger;
        public HttpJob(IHttpClientFactory clientFactory, ILogger<HttpJob> logger)
        {
            _http = clientFactory.CreateClient();
            _http.Timeout = new TimeSpan(0, 30, 0);
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{context.JobDetail.JobDataMap.GetRequestUrl()} http job start");
            string requestUrl = context.JobDetail.JobDataMap.GetRequestUrl();
            int httpMethod = context.JobDetail.JobDataMap.GetHttpMethod();
            string result;
            if (httpMethod == (int)HttpMethodEnum.Post)
            {
                string requestBody = context.JobDetail.JobDataMap.GetRequestBody();
                HttpResponseMessage response = await _http.PostAsync(requestUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _http.GetStringAsync(requestUrl);
            }
            _logger.LogInformation($"{context.JobDetail.JobDataMap.GetRequestUrl()} http job end,the result is ${result}");
        }
    }
}
