using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus
{
    public class ChannelEventSourceStorer : IEventSourceStorer
    {
        private readonly Channel<IEventData> _channel;

        public ChannelEventSourceStorer()
        {
            // 配置通道，设置超出默认容量后进入等待
            var boundedChannelOptions = new BoundedChannelOptions(5000)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            // 创建有限容量通道
            _channel = Channel.CreateBounded<IEventData>(boundedChannelOptions);
        }
        public async ValueTask<IEventData> ReadAsync(CancellationToken cancellationToken)
        {
            // 读取一条事件源
            var eventSource = await _channel.Reader.ReadAsync(cancellationToken);
            return eventSource;
        }

        public async ValueTask WriteAsync(IEventData eventSource)
        {
            await _channel.Writer.WriteAsync(eventSource);
        }
    }
}
