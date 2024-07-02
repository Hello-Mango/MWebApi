using Microsoft.Identity.Client;
using QuickFire.Core.AssemblyFinder;
using QuickFire.Extensions.Quartz;
using QuickFire.Utils;

namespace QuickFireApi.Extensions.ServiceRegister
{
    public static class DynamicServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region dynamic di
            string AssemblySkipPattern = "^System|^Mscorlib|^msvcr120|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted|^libuv|^api-ms|^clrcompression|^clretwrc|^clrjit|^coreclr|^dbgshim|^e_sqlite3|^hostfxr|^hostpolicy|^MessagePack|^mscordaccore|^mscordbi|^mscorrc|sni|sos|SOS.NETCore|^sos_amd64|^SQLitePCLRaw|^StackExchange|^Swashbuckle|WindowsBase|ucrtbase|^DotNetCore.CAP|^MongoDB|^Confluent.Kafka|^librdkafka|^EasyCaching|^RabbitMQ|^Consul|^Dapper|^EnyimMemcachedCore|^Pipelines|^DnsClient|^IdentityModel|^zlib|^NSwag|^Humanizer|^NJsonSchema|^Namotion|^ReSharper|^JetBrains|^NuGet|^ProxyGenerator|^testhost|^MediatR|^Polly|^AspNetCore|^Minio|^SixLabors|^Quartz|^Hangfire|^Handlebars|^Serilog|^WebApiClientCore|^BouncyCastle|^RSAExtensions|^MartinCostello|^Dapr.|^Bogus|^Azure.|^Grpc.|^HealthChecks|^Google|^CommunityToolkit|^Elasticsearch|^ICSharpCode|Enums.NET|^IdentityServer4|JWT|^MathNet|^MK.Hangfire|Mono.TextTemplating|Nest|^NPOI|^Oracle|Spire.Pdf|^FileSignatures";
            var _assemblyFinder = new AppDomainAssemblyFinder { AssemblySkipPattern = AssemblySkipPattern };
            var _typeFinder = new AppDomainTypeFinder(_assemblyFinder);
            services.AddSingleton<IAssemblyFinder>(_assemblyFinder);
            services.AddSingleton<ITypeFinder>(_typeFinder);
            #endregion

            var types = _typeFinder.Find<IServiceRegister>();

            var instances = types.Select(type => (IServiceRegister)Activator.CreateInstance(type)).OrderBy(t => t.OrderId).ToList();
            var context = new ServiceContext(_assemblyFinder, _typeFinder);
            var _serviceActions = new List<Action>();

            instances.ForEach(t => _serviceActions.Add(t.Register(context, services)));
            _serviceActions.ForEach(action => action?.Invoke());

            return services;
        }
    }
}
