using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.EventBus
{
    public class EventData : IEventData
    {
        /// <summary>
        /// 事件发生的时间
        /// </summary>
        public DateTimeOffset EventTime { get; set; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        public object EventSource { get; set; }
        public string Id { get; set; } = new Guid().ToString();

        public EventData()
        {
            EventTime = DateTimeOffset.Now;
            Id = Guid.NewGuid().ToString();
        }
    }

}
