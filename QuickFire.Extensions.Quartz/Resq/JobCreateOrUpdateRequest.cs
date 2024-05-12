namespace QuickFire.Extensions.QuartzResq
{

    public class JobRequest
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 请求方式 1：GET 2：POST
        /// </summary>
        public int HttpMethod { get; set; }
        /// <summary>
        /// 任务请求地址
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 触发类型 1：简单触发 2:CRON触发
        /// </summary>
        public int TriggerType { get; set; }
        /// <summary>
        /// 重复次数，0：无限
        /// </summary>
        public int RepeatCount { get; set; }
        /// <summary>
        /// 间隔时间 触发方式为简单触发时候配置的间隔时间
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 间隔类型 1：秒 2：分 3：时 4：天
        /// </summary>
        public int IntervalType { get; set; }
        /// <summary>
        /// Cron表达式 触发类型为CRON触发时配置
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 开始时间 任务开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间 任务结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 请求参数 请求的参数信息，在body中
        /// </summary>
        public string RequestBody { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否更新任务
        /// </summary>
        public bool IsUpdate { get; set; }
    }
}
