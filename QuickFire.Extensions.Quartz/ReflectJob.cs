using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFire.Extensions.Quartz
{
    public class ReflectJob : IJob
    {
        private readonly string _name;
        private readonly string _group;

        public ReflectJob(string name, string group)
        {
            _name = name;
            _group = group;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            // TODO 任务调度通过反射实现调度指定dll的方法
            // 根据context中的配置job信息获取动态加载的dll信息，例如dll名称、反射的type
            // 判断当前实例池中是否存在该任务信息，若存在，则直接通过实例池获取，若不存在，则进行新增，并且手动执行一次任务

            await Task.FromResult(0);
        }
    }
}
