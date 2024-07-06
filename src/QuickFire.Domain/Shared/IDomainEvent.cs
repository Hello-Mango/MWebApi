using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Shared
{
    public interface IDomainEvent
    {
        // 事件发生的时间戳
        DateTimeOffset OccurredOn { get; }

        //事件的类型名称，有助于事件处理和日志记录
        string EventType { get; }
    }
}
