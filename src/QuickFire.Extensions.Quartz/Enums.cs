using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Quartz
{
    public enum HttpMethodEnum
    {
        [Description("GET")]
        Get = 1,
        [Description("POST")]
        Post = 2
    }

    public enum TriggerTypeEnum
    {
        [Description("Simple")]
        Simple = 1,
        [Description("Cron")]
        Cron = 2
    }
    public enum IntervalTypeEnum
    {
        [Description("Second")]
        Second = 1,
        [Description("Minute")]
        Minute = 2,
        [Description("Hour")]
        Hour = 3,
        [Description("Day")]
        Day = 4
    }
}
