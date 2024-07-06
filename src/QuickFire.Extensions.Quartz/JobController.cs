using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl.Matchers;
using QuickFire.BizException;
using QuickFire.Core;
using QuickFire.Extensions.Quartz.Resp;
using QuickFire.Extensions.QuartzResq;

namespace QuickFire.Extensions.Quartz
{
    [ApiExplorerSettings(GroupName = "QuartzJob")]
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly ILogger<JobController> _logger;
        private readonly IScheduler _scheduler;
        public JobController(ILogger<JobController> logger,
            ISchedulerFactory schedulerFactory
            )
        {
            _logger = logger;
            _scheduler = schedulerFactory.GetScheduler().Result;
        }
        /// <summary>
        /// 创建或者更新任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<string> CreateOrUpdate(JobRequest request)
        {
            request.Name = request.Name.Trim();
            request.Group = request.Group.Trim();

            JobKey key = new JobKey(request.Name, request.Group);
            if (await _scheduler.CheckExists(key))
            {
                if (!request.IsUpdate) throw new Exception("已存在相同名称的任务"); // 新增时，存在相同任务，不创建
                else
                {
                    await _scheduler.DeleteJob(key);    // 更新时，先删除，再创建
                }
            };

            /******Data*****/
            JobDataMap dataMap = new JobDataMap();
            dataMap.Put(DataKeys.HttpMethod, request.HttpMethod);
            dataMap.Put(DataKeys.RequestUrl, request.RequestUrl);
            dataMap.Put(DataKeys.TriggerType, request.TriggerType);
            dataMap.Put(DataKeys.RepeatCount, request.RepeatCount);
            dataMap.Put(DataKeys.Interval, request.Interval);
            dataMap.Put(DataKeys.IntervalType, request.IntervalType);
            dataMap.Put(DataKeys.Cron, request.Cron);
            dataMap.Put(DataKeys.RequestBody, request.RequestBody);
            dataMap.Put(DataKeys.CreateTime, DateTimeOffset.Now.ToString());
            dataMap.Put(DataKeys.StartTime, request.StartTime.ToString());
            dataMap.Put(DataKeys.EndTime, request.EndTime.HasValue ? request.EndTime.Value.ToString() : string.Empty);

            /******Job*****/
            IJobDetail job = JobBuilder.Create<HttpJob>()
                .StoreDurably(true)     // 是否持久化， 无关联触发器时是否移除，false：移除
                .RequestRecovery()  // 重启后是否恢复任务
                .WithDescription(request.Description ?? string.Empty)
                .WithIdentity(request.Name, request.Group)
                .UsingJobData(dataMap)
                .Build();

            /******Trigger*****/
            TriggerBuilder builder = TriggerBuilder.Create()
                .WithIdentity(request.Name, request.Group)
                .StartAt(request.StartTime)
                .ForJob(job);
            if (request.EndTime.HasValue)
            {
                builder.EndAt(request.EndTime.Value);
            }
            if (request.TriggerType == (int)TriggerTypeEnum.Simple)
            {
                builder.WithSimpleSchedule(simple =>
                {
                    if (request.IntervalType == (int)IntervalTypeEnum.Second)
                    {
                        simple.WithIntervalInSeconds(request.Interval);
                    }
                    if (request.IntervalType == (int)IntervalTypeEnum.Minute)
                    {
                        simple.WithIntervalInMinutes(request.Interval);
                    }
                    if (request.IntervalType == (int)IntervalTypeEnum.Hour)
                    {
                        simple.WithIntervalInHours(request.Interval);
                    }
                    if (request.IntervalType == (int)IntervalTypeEnum.Day)
                    {
                        simple.WithIntervalInHours(request.Interval * 24);
                    }
                    if (request.RepeatCount > 0)
                    {
                        simple.WithRepeatCount(request.RepeatCount);
                    }
                    else
                    {
                        simple.RepeatForever();
                    }
                    simple.WithMisfireHandlingInstructionFireNow();
                });
            }
            else
            {
                builder.WithCronSchedule(request.Cron, cron =>
                {
                    cron.WithMisfireHandlingInstructionFireAndProceed();
                });
            }
            ITrigger trigger = builder.Build();
            //await _scheduler.AddJob(job, false); // 加入调度，并持久化
            await _scheduler.ScheduleJob(job, trigger);
            await _scheduler.PauseJob(key);
            return "success";
        }

        /// <summary>
        /// 触发一次任务
        /// </summary>
        /// <param name="name">job名称</param>
        /// <param name="group">job组</param>
        /// <returns></returns>
        [HttpPost("{group}/{name}")]
        public async Task Trigger([FromRoute] string name, [FromRoute] string group)
        {
            await _scheduler.TriggerJob(new JobKey(name, group));
        }
        /// <summary>
        /// 获取所有任务或者根据group获取当前组的任务
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("{group}")]
        public async Task<IEnumerable<JobResponse>> Jobs([FromRoute] string group)
        {
            GroupMatcher<JobKey> matcher = GroupMatcher<JobKey>.AnyGroup();
            if (!string.IsNullOrWhiteSpace(group))
            {
                matcher = GroupMatcher<JobKey>.GroupEquals(group);
            }

            IReadOnlyCollection<JobKey> jobKeys = await _scheduler.GetJobKeys(matcher); // 根据分组查询条件 获取所有JobKey
            List<JobResponse> items = new List<JobResponse>();

            foreach (JobKey key in jobKeys)
            {
                IJobDetail? job = await _scheduler.GetJobDetail(key);
                if (job == null) continue;

                JobResponse item = new JobResponse
                {
                    Name = job.Key.Name,
                    Group = job.Key.Group,
                    TriggerState = TriggerState.Complete.ToString(),
                    HttpMethod = job.JobDataMap.GetHttpMethod(),
                    RequestUrl = job.JobDataMap.GetRequestUrl(),
                    TriggerType = job.JobDataMap.GetTriggerType(),
                    Interval = job.JobDataMap.GetInterval(),
                    IntervalType = job.JobDataMap.GetIntervalType(),
                    RepeatCount = job.JobDataMap.GetRepeatCount(),
                    Cron = job.JobDataMap.GetCron(),
                    RequestBody = job.JobDataMap.GetRequestBody(),
                    Description = job.Description,
                    CreateTime = job.JobDataMap.GetCreateTime(),
                    StartTime = job.JobDataMap.GetStartTime(),
                    EndTime = job.JobDataMap.GetEndTime(),
                    LastException = job.JobDataMap.GetLastException()
                };

                IReadOnlyCollection<ITrigger> triggers = await _scheduler.GetTriggersOfJob(key);
                ITrigger? trigger = triggers.FirstOrDefault();   // 获取当前job关联的第一个 trigger
                if (trigger != null)
                {
                    TriggerState triggerState = await _scheduler.GetTriggerState(trigger.Key);  // trigger 状态

                    /****计算时间差***/
                    DateTimeOffset? prevFire = trigger.GetPreviousFireTimeUtc();
                    DateTimeOffset? nextFire = trigger.GetNextFireTimeUtc();
                    TimeSpan span = TimeSpan.FromSeconds(0);
                    if (prevFire.HasValue && nextFire.HasValue)
                    {
                        span = (nextFire.Value - prevFire.Value);
                    }
                    item.TriggerState = triggerState.ToString();
                    item.FirePlan = $"{span.Days}天{span.Hours}小时{span.Minutes}分{span.Seconds}秒";    // 执行频率
                    item.PrevFireTime = prevFire;
                    item.NextFireTime = nextFire;
                };

                items.Add(item);
            }

            return items;
        }
        /// <summary>
        /// 获取单个任务
        /// </summary>
        /// <param name="name">job名称</param>
        /// <param name="group">job组</param>
        /// <returns></returns>
        /// <exception cref="Exception422"></exception>
        [HttpGet("{group}/{name}")]
        public async Task<JobResponse> Job([FromRoute] string name, [FromRoute] string group)
        {
            var jobKey = new JobKey(name, group);
            IJobDetail? job = await _scheduler.GetJobDetail(jobKey);
            if (job == null)
            {
                throw new Exception422("任务不存在");
            }
            JobResponse item = new JobResponse
            {
                Name = job.Key.Name,
                Group = job.Key.Group,
                TriggerState = TriggerState.Complete.ToString(),
                HttpMethod = job.JobDataMap.GetHttpMethod(),
                RequestUrl = job.JobDataMap.GetRequestUrl(),
                TriggerType = job.JobDataMap.GetTriggerType(),
                Interval = job.JobDataMap.GetInterval(),
                IntervalType = job.JobDataMap.GetIntervalType(),
                RepeatCount = job.JobDataMap.GetRepeatCount(),
                Cron = job.JobDataMap.GetCron(),
                RequestBody = job.JobDataMap.GetRequestBody(),
                Description = job.Description,
                CreateTime = job.JobDataMap.GetCreateTime(),
                StartTime = job.JobDataMap.GetStartTime(),
                EndTime = job.JobDataMap.GetEndTime(),
                LastException = job.JobDataMap.GetLastException()
            };

            IReadOnlyCollection<ITrigger> triggers = await _scheduler.GetTriggersOfJob(jobKey);
            ITrigger? trigger = triggers.FirstOrDefault();   // 获取当前job关联的第一个 trigger
            if (trigger != null)
            {
                TriggerState triggerState = await _scheduler.GetTriggerState(trigger.Key);  // trigger 状态

                /****计算时间差***/
                DateTime? prevFire = trigger.GetPreviousFireTimeUtc()?.LocalDateTime;
                DateTime? nextFire = trigger.GetNextFireTimeUtc()?.LocalDateTime;
                TimeSpan span = TimeSpan.FromSeconds(0);
                if (prevFire.HasValue && nextFire.HasValue)
                {
                    span = (nextFire.Value - prevFire.Value);
                }
                item.TriggerState = triggerState.ToString();
                item.FirePlan = $"{span.Days}天{span.Hours}小时{span.Minutes}分{span.Seconds}秒";    // 执行频率
                item.PrevFireTime = prevFire;
                item.NextFireTime = nextFire;
            };

            return item;
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpDelete("{group}/{name}")]
        public async Task<string> Delete([FromRoute] string name, [FromRoute] string group)
        {
            await _scheduler.DeleteJob(new JobKey(name, group));
            return "success";
        }
        /// <summary>
        /// 暂停当前任务执行
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="group">任务组</param>
        /// <returns></returns>
        [HttpPost("{group}/{name}")]
        public async Task<string> Pause([FromRoute] string name, [FromRoute] string group)
        {
            await _scheduler.PauseJob(new JobKey(name, group));
            return "success";
        }
        /// <summary>
        /// 重新启动任务
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost("{group}/{name}")]
        public async Task<string> Resume([FromRoute] string name, [FromRoute] string group)
        {
            await _scheduler.ResumeJob(new JobKey(name, group));
            return "success";
        }
    }
}
