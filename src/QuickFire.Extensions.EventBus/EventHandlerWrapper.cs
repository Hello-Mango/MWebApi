// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus

/// <summary>
/// 事件处理程序包装类
/// </summary>
/// <remarks>主要用于主机服务启动时将所有处理程序和事件 Id 进行包装绑定</remarks>
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