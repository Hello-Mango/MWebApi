using QuickFire.Domain.Shared;
using QuickFire.Infrastructure.Repository;
using QuickFire.Infrastructure;

namespace QuickFireApi.Extensions
{
    public static class DataBaseExtension
    {
        public static IServiceCollection AddDataBase(this IServiceCollection service)
        {
            service.AddScoped(typeof(IUnitOfWork<>), typeof(EFUnitOfWork<>));
            service.AddScoped(typeof(IRepository<>), typeof(LongIdRepository<>));
            service.AddScoped(typeof(IReadOnlyRepository<>), typeof(LongIdReadOnlyRepository<>));
            service.AddDbContext<ApplicationDbContext>();
            return service;
        }
    }
}