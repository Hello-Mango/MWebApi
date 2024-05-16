using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QuickFire.Extensions.EventBus
{
    public interface IEventSourceStorer
    {
        /// <summary>
        /// 将事件源写入存储器
        /// </summary>
        /// <param name="eventSource">事件源对象</param>
        /// <param name="cancellationToken">取消任务 Token</param>
        /// <returns><see cref="ValueTask"/></returns>
        ValueTask WriteAsync(IEventData eventSource);

        /// <summary>
        /// 从存储器中读取一条事件源
        /// </summary>
        /// <param name="cancellationToken">取消任务 Token</param>
        /// <returns>事件源对象</returns>
        ValueTask<IEventData> ReadAsync(CancellationToken cancellationToken);
    }
}
