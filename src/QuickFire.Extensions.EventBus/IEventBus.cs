using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus
{
    public interface IEventBus
    {
        #region 接口注册
        void Register<TEventData>(Type handlerType) where TEventData : IEventData;
        void Register(Type eventType, Type handlerType);
        void Register(string eventType, Type handlerType);
        #endregion

        #region 接口取消注册
        void Unregister<TEventData>(Type handler) where TEventData : IEventData;
        void Unregister(Type eventType, Type handlerType);
        void Unregister(string eventType, Type handlerType);
        #endregion


        void Publish(string pubKey, IEventData eventData);
        Task PublishAsync(string pubKey, IEventData eventData);
        void Publish<TEventData>(TEventData eventData) where TEventData : IEventData;
        Task PublishAsync<TEventData>(TEventData eventData) where TEventData : IEventData;
    }
}
