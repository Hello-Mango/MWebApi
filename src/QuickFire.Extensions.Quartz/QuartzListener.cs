using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Quartz
{
    public class DefaultSchedulerListener : SchedulerListenerSupport
    {
        private readonly ILogger<DefaultSchedulerListener> _logger;
        public DefaultSchedulerListener(ILogger<DefaultSchedulerListener> logger)
        {
            _logger = logger;
        }
        public override Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            _logger.LogError(cause.InnerException, "Quartz.NET ISchedulerListener SchedulerError() log：" + msg);    // 记录错误日志

            return base.SchedulerError(msg, cause, cancellationToken);
        }
    }

    public class DefaultJobListener : JobListenerSupport
    {
        public event Action<IJobExecutionContext> JobStarted;
        public event Action<IJobExecutionContext> JobFinished;
        private readonly ILogger<DefaultJobListener> _logger;
        public DefaultJobListener(
            ILogger<DefaultJobListener> logger)
        {
            _logger = logger;
        }
        public override string Name => "QuickFire.JobListener";

        /// <summary>
        /// 执行被否决
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogError($"Quartz.NET IJobListener JobExecutionVetoed()方法日志：Name = {context.JobDetail.Key.Name}, Group = {context.JobDetail.Key.Group}");    // 记录错误日志
            return base.JobExecutionVetoed(context, cancellationToken);
        }

        /// <summary>
        /// 执行完毕
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            string cacheKey = $"{context.JobDetail.Key.Group}.{context.JobDetail.Key.Name}.Exception";  // 缓存键

            if (jobException != null && !string.IsNullOrEmpty(jobException.Message))
            {
                Exception? ex = jobException.InnerException;
                while (ex != null && ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                _logger.LogWarning($"任务调度异常：Name = {context.JobDetail.Key.Name}, Group = {context.JobDetail.Key.Group},ExMsg={ex?.Message},ExStack={ex?.StackTrace}");
            }
            JobFinished?.Invoke(context);
            return base.JobWasExecuted(context, jobException, cancellationToken);
        }

        /// <summary>
        /// 执行即将开始
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            JobStarted?.Invoke(context);
            return base.JobToBeExecuted(context, cancellationToken);
        }
    }

    public class DefaultTriggerListener : TriggerListenerSupport
    {
        private readonly ILogger<DefaultTriggerListener> _logger;
        public DefaultTriggerListener(ILogger<DefaultTriggerListener> logger)
        {
            _logger = logger;
        }
        public override string Name => "QuickFire.TriggerListener";

        /// <summary>
        /// 错过触发，可能原因是 任务逻辑处理时间太长，应保持job业务逻辑尽量过短
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            _logger.LogError($"Quartz.NET ITriggerListener TriggerMisfired()方法日志：Name = {trigger.JobKey.Name}，Group = {trigger.JobKey.Group}");    // 记录错误日志
            return base.TriggerMisfired(trigger, cancellationToken);
        }
    }
}
