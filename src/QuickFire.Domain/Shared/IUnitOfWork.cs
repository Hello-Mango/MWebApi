using QuickFire.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Shared
{
    public interface IUnitOfWork<T>
    {
        public IRepository<TEntity>? GetRepository<TEntity>() where TEntity : BaseEntity;
        public IRepository<TEntity, TKey>? GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;

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
