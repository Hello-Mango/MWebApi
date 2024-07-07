using Microsoft.EntityFrameworkCore;
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

        public bool IsTransactionActive { get; private set; }

        public void BeginTransaction()
        {
            if (IsTransactionActive) return;

            IsTransactionActive = true;
            _dbContext.Database.BeginTransaction();
        }

        public Task BeginTransactionAsync()
        {
            if (IsTransactionActive) return Task.CompletedTask;

            IsTransactionActive = true;
            return _dbContext.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            IsTransactionActive=false;
            _dbContext.Database.CommitTransaction();
        }

        public Task CommitTransactionAsync()
        {
            IsTransactionActive = false;
            return _dbContext.Database.CommitTransactionAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        //public IRepository<TEntity> GetBaseRepository<TEntity>() where TEntity : BaseEntityLId
        //{
        //    var repository = _serviceProvider.GetService<IRepository<TEntity>>();
        //    repository!.CheckNull(nameof(IRepository<TEntity>));
        //    return repository!;
        //}

        //public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity<long>
        //{
        //    var repository = _serviceProvider.GetService<IRepository<TEntity>>();
        //    repository!.CheckNull(nameof(IRepository<TEntity>));
        //    return repository!;
        //}

        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            var repository = _serviceProvider.GetService<IRepository<TEntity, TKey>>();
            repository!.CheckNull(nameof(IRepository<TEntity, TKey>));
            return repository!;
        }

        public void RollbackTransaction()
        {
            IsTransactionActive = false;
            _dbContext.Database.RollbackTransaction();
        }

        public Task RollbackTransactionAsync()
        {
            IsTransactionActive = false;
            return _dbContext.Database.RollbackTransactionAsync();
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
