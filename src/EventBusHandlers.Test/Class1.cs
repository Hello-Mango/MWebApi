using System;
using System.Threading.Tasks;
using QuickFire.Extensions.EventBus;

namespace EventBusHandlers.Test
{
    public class Class1 : IEventHandler<Test>
    {
        public void Handle(Test eventData)
        {
            Console.WriteLine(eventData.EventSource);
        }
    }
    public class Test : EventData
    {
    }


}
