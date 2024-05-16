using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus
{
    public interface IEventHandler
    {

    }
    public interface IEventHandler<TEventData> : IEventHandler where TEventData : IEventData
    {
        /// <summary>
        /// 事件处理器处理具体事件的接口
        /// </summary>
        /// <param name="eventData"></param>
        Task Handle(TEventData eventData);
    }
}
