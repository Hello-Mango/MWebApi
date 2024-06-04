using QuickFire.Extensions.EventBus;
using System;
using System.Threading.Tasks;

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
