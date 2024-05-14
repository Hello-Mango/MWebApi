using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.EventBus
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


        void Trigger(string pubKey, IEventData eventData);
        Task TriggerAsync(string pubKey, IEventData eventData);
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;
    }
}
