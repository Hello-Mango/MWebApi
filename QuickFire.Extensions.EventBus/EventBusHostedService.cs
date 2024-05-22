using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus
{
   
    public class EventBusHostedService : BackgroundService
    {
        private readonly IEventSourceStorer _eventSourceStorer;
        private ConcurrentDictionary<Type, List<Func<IEventData, Task>>> dicEvent = new ConcurrentDictionary<Type, List<Func<IEventData, Task>>>();
        private readonly ILogger _logger;
        public EventBusHostedService(
            ILogger<EventBusHostedService> logger,
            IEventSourceStorer eventSourceStorer)
        {
            _logger = logger;
            _eventSourceStorer = eventSourceStorer;

            //自动扫描类型并且注册
            foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                var ass = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
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
                                    //Type constructedType = item.MakeGenericType(item2);
                                    var obj = Activator.CreateInstance(item);
                                    MethodInfo methodInfo = item.GetMethod("Handle");
                                    methodInfo.GetParameters()[0].GetType();

                                    var delegateType = typeof(Action<>).MakeGenericType(item2);
                                    //var res = Delegate.CreateDelegate(delegateType, target, methodInfo);

                                    var handler = methodInfo.CreateDelegate(delegateType, obj);
                                    //通过反射生成item的实例，该实例的基类为handler
                                    //Register(item2, handler);
                                }
                            }
                        }
                    }
                }
            }

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 监听服务是否取消
            while (!stoppingToken.IsCancellationRequested)
            {
                // 执行具体任务
                await BackgroundProcessing(stoppingToken);
            }
        }

        /// <summary>
        /// 后台调用处理程序
        /// </summary>
        /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
        /// <returns><see cref="Task"/> 实例</returns>
        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            // 从事件存储器中读取一条
            var eventSource = await _eventSourceStorer.ReadAsync(stoppingToken);
            if (eventSource == null)
            {
                return;
            }
            Type eventType = eventSource.GetType();
            if (dicEvent.ContainsKey(eventType))
            {
                foreach (Func<IEventData, Task> item in dicEvent[eventType])
                {
                    await item.Invoke(eventSource);
                }
            }
        }

        #region 注册事件
        public void Register<TEventData>(Func<IEventData, Task> handlerType) where TEventData : IEventData
        {
            //将数据存储到mapDic
            var dataType = typeof(TEventData);
            Register(dataType, handlerType);
        }
        public void Register(Type pubKey, Func<IEventData, Task> handlerType)
        {
            //将数据存储到dicEvent
            dicEvent[pubKey] = new List<Func<IEventData, Task>>();
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
            if (dicEvent.Keys.Contains(eventType))
            {
                if (dicEvent[eventType].Exists(p => p.GetType() == handlerType))
                {
                    dicEvent[eventType].Remove(dicEvent[eventType].Find(p => p.GetType() == handlerType));
                }
            }
        }
        #endregion
    }
}
