using CronExpressionDescriptor;

namespace QuickFire.Extensions.Quartz.Resp
{
    public class JobResponse
    {
        /// <summary>
        /// job名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// job分组
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 当前状态 0：Normal，1：Paused，2：Complete，3：Error 4：Blocked，5：None
        /// </summary>
        public string TriggerState { get; set; }
        /// <summary>
        /// 请求方式  1：GET 2：POST
        /// </summary>
        public int HttpMethod { get; set; }
        //public string HttpMethodName
        //{
        //    get
        //    {
        //        return ((HttpMethodEnum)HttpMethod).ToText();
        //    }
        //}
        /// <summary>
        /// 任务请求地址
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 任务停止时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 前一次执行时间
        /// </summary>
        public DateTime? PrevFireTime { get; set; }
        /// <summary>
        /// 下一次执行时间
        /// </summary>
        public DateTime? NextFireTime { get; set; }
        /// <summary>
        /// 执行计划/频率
        /// </summary>
        public string FirePlan { get; set; }
        /// <summary>
        /// 触发类型 1：简单触发 2:CRON触发
        /// </summary>
        public int TriggerType { get; set; }
        //public string TriggerTypeName
        //{
        //    get
        //    {
        //        return ((TriggerTypeEnum)TriggerType).ToText();
        //    }
        //}
        /// <summary>
        /// 重复次数，0：无限
        /// </summary>
        public int RepeatCount { get; set; }
        /// <summary>
        /// 间隔时间
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 间隔类型 1：秒 2：分 3：时 4：天
        /// </summary>
        public int IntervalType { get; set; }
        //public string IntervalTypeName
        //{
        //    get
        //    {
        //        return ((IntervalTypeEnum)IntervalType).ToText();
        //    }
        //}
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// Cron表达式 说明
        /// </summary>
        public string CronDesc
        {
            get
            {
                if (TriggerType == (int)TriggerTypeEnum.Cron && !string.IsNullOrWhiteSpace(Cron))
                {
                    try
                    {
                        return ExpressionDescriptor.GetDescription(Cron, new Options()
                        {
                            Locale = "zh-Hans"
                        });
                    }
                    catch
                    {
                        // ignore
                    }
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 请求参数 请求的参数信息，在body中
        /// </summary>
        public string RequestBody { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后一次异常信息
        /// </summary>
        public string LastException { get; set; }
    }
}
