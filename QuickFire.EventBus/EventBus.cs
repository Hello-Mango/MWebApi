using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace QuickFire.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventBus> _logger;
        public EventBus(ILogger<EventBus> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            InitRegister();
        }
        //注册以及取消注册的时候需要加锁处理
        private readonly object obj = new object();
        private ConcurrentDictionary<string, List<Type>> dicEvent = new ConcurrentDictionary<string, List<Type>>();
        #region 注册事件
        public void Register<TEventData>(Type handlerType) where TEventData : IEventData
        {
            //将数据存储到mapDic
            var dataType = typeof(TEventData).FullName;
            Register(dataType, handlerType);
        }
        public void Register(Type eventType, Type handlerType)
        {
            var dataType = eventType.FullName;
            Register(dataType, handlerType);
        }
        public void Register(string pubKey, Type handlerType)
        {
            lock (obj)
            {
                //将数据存储到dicEvent
                if (dicEvent.Keys.Contains(pubKey) == false)
                {
                    dicEvent[pubKey] = new List<Type>();
                }
                if (dicEvent[pubKey].Exists(p => p.GetType() == handlerType) == false)
                {
                    //IEventHandler obj = Activator.CreateInstance(handlerType) as IEventHandler;
                    dicEvent[pubKey].Add(handlerType);
                }
            }
        }

        #endregion

        #region 取消事件注册
        public void Unregister<TEventData>(Type handler) where TEventData : IEventData
        {
            var dataType = typeof(TEventData);
            Unregister(dataType, handler);
        }

        public void Unregister(Type eventType, Type handlerType)
        {
            string _key = eventType.FullName;
            Unregister(_key, handlerType);
        }
        public void Unregister(string eventType, Type handlerType)
        {
            lock (obj)
            {
                if (dicEvent.Keys.Contains(eventType))
                {
                    if (dicEvent[eventType].Exists(p => p.GetType() == handlerType))
                    {
                        dicEvent[eventType].Remove(dicEvent[eventType].Find(p => p.GetType() == handlerType));
                    }
                }
            }
        }
        #endregion

        #region Trigger触发
        //trigger时候需要记录到数据库
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            var dataType = eventData.GetType().FullName;
            //获取当前的EventData绑定的所有Handler
            Notify(dataType, eventData).Wait();
        }

        public void Trigger(string pubKey, IEventData eventData)
        {
            //获取当前的EventData绑定的所有Handler
            Notify(pubKey, eventData).Wait();
        }
        public async Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            var dataType = eventData.GetType().FullName;
            await Notify(dataType, eventData);
        }
        public async Task TriggerAsync(string pubKey, IEventData eventData)
        {
            await Notify(pubKey, eventData);
        }
        //通知每成功执行一个就需要记录到数据库
        private async Task Notify<TEventData>(string eventType, TEventData eventData) where TEventData : IEventData
        {
            //获取当前的EventData绑定的所有Handler
            var handlerTypes = dicEvent[eventType];
            foreach (var handlerType in handlerTypes)
            {
                var resolveObj = _serviceProvider.GetService(handlerType);
                if (resolveObj != null)
                {
                    IEventHandler<TEventData>? handler = resolveObj as IEventHandler<TEventData>;
                    if (handler != null)
                    {
                        //异步执行handler!.Handle(eventData);
                        await Task.Factory.StartNew(() => handler.Handle(eventData));
                    }
                    else
                    {
                        _logger.LogError($"未找到{handlerType.FullName}的实例2");
                    }
                }
                else
                {
                    _logger.LogError($"未找到{handlerType.FullName}的实例1");
                }
            }
        }
        #endregion

        public void InitRegister()
        {
            if (dicEvent.Count > 0)
            {
                return;
            }
            //_iresolve = ioc_container;
            dicEvent = new ConcurrentDictionary<string, List<Type>>();
            //自动扫描类型并且注册
            foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                var ass = AssemblyLoadContext.GetLoadContext(typeof(EventBus).Assembly).LoadFromAssemblyPath(file);
                //var ass = Assembly.LoadFrom(file);
                foreach (var item in ass.GetTypes().Where(p => p.GetInterfaces().Contains(typeof(IEventHandler))))
                {
                    if (item.IsClass)
                    {
                        foreach (var item1 in item.GetInterfaces())
                        {
                            foreach (var item2 in item1.GetGenericArguments())
                            {
                                if (item2.GetInterfaces().Contains(typeof(IEventData)))
                                {
                                    Register(item2, item);
                                }
                            }
                        }
                    }
                }
            }
        }


    }
}
