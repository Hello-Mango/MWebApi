using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Extensions.EventBus
{
    public class ChannelEventPublisher : IEventPublisher
    {
        private readonly IEventSourceStorer _eventSourceStorer;
        public ChannelEventPublisher(IEventSourceStorer eventSourceStorer)
        {
            _eventSourceStorer = eventSourceStorer;
        }
        public async Task PublishAsync(IEventData eventSource)
        {
            await _eventSourceStorer.WriteAsync(eventSource);
        }
    }
}
