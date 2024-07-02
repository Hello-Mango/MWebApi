using Microsoft.Extensions.DependencyInjection.Extensions;
using QuickFire.Core.AssemblyFinder;
using QuickFire.Core.Dependency;
using QuickFire.Utils;
using System.Reflection;
using System.Runtime.InteropServices;

namespace QuickFireApi.Extensions.ServiceRegister
{
    public class DependencyServiceRegistrar : IServiceRegister
    {
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderId => 100;

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceContext">服务上下文</param>
        public Action Register(ServiceContext serviceContext, IServiceCollection services)
        {
            return () =>
            {
                RegisterDependency<ISingletonDependency>(services, serviceContext.TypeFinder, ServiceLifetime.Singleton);
                RegisterDependency<IScopeDependency>(services, serviceContext.TypeFinder, ServiceLifetime.Scoped);
                RegisterDependency<ITransientDependency>(services, serviceContext.TypeFinder, ServiceLifetime.Transient);
            };
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        private void RegisterDependency<TDependencyInterface>(IServiceCollection services, ITypeFinder finder, ServiceLifetime lifetime)
        {
            var types = GetTypes<TDependencyInterface>(finder);
            var result = FilterTypes(types);
            foreach (var item in result)
                RegisterType(services, item.Item1, item.Item2, lifetime);
        }

        /// <summary>
        /// 获取接口类型和实现类型列表
        /// </summary>
        private List<(Type, Type)> GetTypes<TDependencyInterface>(ITypeFinder finder)
        {
            var result = new List<(Type, Type)>();
            var classTypes = finder.Find<TDependencyInterface>();
            foreach (var classType in classTypes)
            {
                var interfaceTypes = ReflectionUtils.GetInterfaceTypes(classType, typeof(TDependencyInterface));
                interfaceTypes.ForEach(interfaceType => result.Add((interfaceType, classType)));
            }
            return result;
        }

        /// <summary>
        /// 过滤类型
        /// </summary>
        private List<(Type, Type)> FilterTypes(List<(Type, Type)> types)
        {
            var result = new List<(Type, Type)>();
            foreach (var group in types.GroupBy(t => t.Item1))
            {
                result.Add(group.First());
            }
            return result;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        private void RegisterType(IServiceCollection services, Type interfaceType, Type classType, ServiceLifetime lifetime)
        {
            services.TryAdd(new ServiceDescriptor(interfaceType, classType, lifetime));
        }
    }


    public interface IServiceRegister
    {
        /// <summary>
        /// 排序号
        /// </summary>
        int OrderId { get; }

        /// <summary>
        /// 注册服务,该操作在启动开始时执行,如果需要延迟执行某些操作,可在返回的Action中执行,它将在启动最后执行
        /// </summary>
        /// <param name="context">服务上下文</param>
        Action Register(ServiceContext context, IServiceCollection services);
    }
    public class ServiceContext
    {
        /// <summary>
        /// 初始化服务上下文
        /// </summary>
        /// <param name="assemblyFinder">程序集查找器</param>
        /// <param name="typeFinder">类型查找器</param>
        public ServiceContext(IAssemblyFinder assemblyFinder, ITypeFinder typeFinder)
        {
            AssemblyFinder = assemblyFinder ?? throw new ArgumentNullException(nameof(assemblyFinder));
            TypeFinder = typeFinder ?? throw new ArgumentNullException(nameof(typeFinder));
        }


        /// <summary>
        /// 程序集查找器
        /// </summary>
        public IAssemblyFinder AssemblyFinder { get; }

        /// <summary>
        /// 类型查找器
        /// </summary>
        public ITypeFinder TypeFinder { get; }
    }
}
