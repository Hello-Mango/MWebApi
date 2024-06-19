using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QucikFire.Extensions;
using QuickFire.Extensions.EventBus;

namespace EventBusHandlers.Test
{
    public class Class1 : IEventHandler<Test>
    {
        private IServiceProvider serviceProvider1;
        public Class1(IServiceProvider serviceProvider)
        {
            serviceProvider1 = serviceProvider;
        }
        public void Handle(Test eventData)
        {
            var temp = serviceProvider1.GetService<ICacheService>();
            temp.Set("test", "test");
            string res = temp.Get("test");
            Console.WriteLine(eventData.EventSource);
        }
    }
    public class Test : EventData
    {
    }


}
