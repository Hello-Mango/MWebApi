using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Quartz
{
    internal sealed class CacheKeys
    {
        public static readonly string AllGroupKey = "AllGroupKey";
        public static readonly string SchedulerInfoKey = "SchedulerInfoKey";
    }

    internal sealed class DataKeys
    {
        public static readonly string HttpMethod = "HttpMethod";
        public static readonly string RequestUrl = "RequestUrl";
        public static readonly string TriggerType = "TriggerType";
        public static readonly string RepeatCount = "RepeatCount";
        public static readonly string Interval = "Interval";
        public static readonly string IntervalType = "IntervalType";
        public static readonly string Cron = "Cron";
        public static readonly string RequestBody = "RequestBody";
        public static readonly string CreateTime = "CreateTime";
        public static readonly string StartTime = "StartTime";
        public static readonly string EndTime = "EndTime";

        public static readonly string LastException = "LastException";
        public static readonly string LogList = "LogList";
    }
}
