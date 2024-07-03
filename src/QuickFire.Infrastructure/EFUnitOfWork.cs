using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuickFire.Core;
using QuickFire.Domain.Entity.Base;
using QuickFire.Domain.Shared;
using QuickFire.Infrastructure.Repository;
using QuickFire.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure
{
    public class EFUnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        private readonly T _dbContext;
        private readonly IServiceProvider _serviceProvider;
        public EFUnitOfWork(T dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public bool IsTransactionActive { get; private set; }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
            IsTransactionActive = true;
        }

        public Task BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public Task CommitTransactionAsync()
        {
            return _dbContext.Database.CommitTransactionAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var repository = _serviceProvider.GetService<IRepository<TEntity>>();
            repository!.CheckNull(nameof(IRepository<TEntity>));
            return repository!;
        }

        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var repository = _serviceProvider.GetService<IRepository<TEntity, TKey>>();
            repository!.CheckNull(nameof(IRepository<TEntity, TKey>));
            return repository!;
        }

        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public Task RollbackTransactionAsync()
        {
            return _dbContext.Database.RollbackTransactionAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
