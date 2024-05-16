using QuickFire.Extensions.EventBus;
using System;
using System.Threading.Tasks;

namespace EventBusHandlers.Test
{
    public class Class1 : IEventHandler<QuickFire.Extensions.EventBus.Test>
    {
        public Task Handle(QuickFire.Extensions.EventBus.Test eventData)
        {
            Console.WriteLine(eventData.EventSource);
            return Task.CompletedTask;
        }
    }


}
