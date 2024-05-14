using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.EventBus
{
    public interface IEventData
    {
        string Id { get; set; }
        DateTimeOffset EventTime { get; set; }
        object EventSource { get; set; }
    }
}
