using Microsoft.EntityFrameworkCore.Storage;

namespace QuickFire.Domain.Shared
{
    public interface IUnitOfWork<T>
    {
        public IRepository<TEntity> GetLongRepository<TEntity>() where TEntity : class, IEntity<long>;
        public IRepository<TEntity, string> GetStringRepository<TEntity>() where TEntity : class, IEntity<string>;
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;

        public Task<int> SaveChangesAsync();

        public void Dispose();

        public IDbContextTransaction BeginTransaction();
        public Task<IDbContextTransaction> BeginTransactionAsync();

        public void CommitTransaction();
        public Task CommitTransactionAsync();

        public Task RollbackTransactionAsync();
        public void RollbackTransaction();

        public bool IsTransactionActive { get; }
    }
}
