using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using QuickFire.Core;
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
        private IDbContextTransaction dbContextTransaction;
        public bool IsTransactionActive { get; private set; }

        public IDbContextTransaction BeginTransaction()
        {
            if (IsTransactionActive) return dbContextTransaction;

            IsTransactionActive = true;
            dbContextTransaction = _dbContext.Database.BeginTransaction();
            return dbContextTransaction;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (IsTransactionActive)
            {
                return dbContextTransaction;
            }

            IsTransactionActive = true;
            dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();
            return dbContextTransaction;
        }

        public void CommitTransaction()
        {
            IsTransactionActive = false;
            dbContextTransaction.Commit();
        }

        public async Task CommitTransactionAsync()
        {
            IsTransactionActive = false;
            await dbContextTransaction.CommitAsync();
            return;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            var repository = _serviceProvider.GetService<IRepository<TEntity, TKey>>();
            repository!.CheckNull(nameof(IRepository<TEntity, TKey>));
            return repository!;
        }

        public void RollbackTransaction()
        {
            IsTransactionActive = false;
            dbContextTransaction.Rollback();
        }

        public async Task RollbackTransactionAsync()
        {
            IsTransactionActive = false;
            await dbContextTransaction.RollbackAsync();
            return;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public IRepository<TEntity> GetLongRepository<TEntity>() where TEntity : class, IEntity<long>
        {
            var repository = _serviceProvider.GetService<IRepository<TEntity>>();
            repository!.CheckNull(nameof(IRepository<TEntity>));
            return repository!;
        }

        public IRepository<TEntity, string> GetStringRepository<TEntity>() where TEntity : class, IEntity<string>
        {
            var repository = _serviceProvider.GetService<IRepository<TEntity, string>>();
            repository!.CheckNull(nameof(IRepository<TEntity, string>));
            return repository!;
        }
    }
}
