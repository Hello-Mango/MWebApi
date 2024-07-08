using QuickFire.Core;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Shared
{
    public interface IUnitOfWork
    {
        public IRepository<TEntity> GetLongRepository<TEntity>() where TEntity : class, IEntity<long>;
        public IRepository<TEntity, string> GetStringRepository<TEntity>() where TEntity : class, IEntity<string>;
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;

        public Task<int> SaveChangesAsync();

        public void Dispose();

        public void BeginTransaction();
        public Task BeginTransactionAsync();

        public void CommitTransaction();
        public Task CommitTransactionAsync();

        public Task RollbackTransactionAsync();
        public void RollbackTransaction();

        public bool IsTransactionActive { get; }
    }
}
