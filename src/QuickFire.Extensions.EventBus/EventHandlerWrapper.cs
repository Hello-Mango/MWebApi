using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus
{
    internal sealed class EventHandlerWrapper
    {
        /// <summary>
        /// 事件处理程序
        /// </summary>
        internal Func<IEventData, Task> Handler { get; set; }

        /// <summary>
        /// 触发的方法
        /// </summary>
        internal MethodInfo HandlerMethod { get; set; }
    }
}