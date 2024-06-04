using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus
{
    public interface IEventPublisher
    {
        /// <summary>
        /// 发布一条消息
        /// </summary>
        /// <param name="eventSource">事件源</param>
        /// <returns><see cref="Task"/> 实例</returns>
        Task PublishAsync(IEventData eventSource);
    }
}
