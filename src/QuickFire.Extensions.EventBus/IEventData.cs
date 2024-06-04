using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Extensions.EventBus
{
    public interface IEventData
    {
        string Id { get; set; }
        DateTimeOffset EventTime { get; set; }
        object EventSource { get; set; }
    }
}
