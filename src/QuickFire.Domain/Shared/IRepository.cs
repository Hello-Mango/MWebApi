using System;
using System.Linq.Expressions;

namespace QuickFire.Domain.Shared
{
    public interface IRepository : IDisposable
    { }

    public interface IRepository<TEntity,TKey> where TEntity : class
    {
        TEntity Add(TEntity t);
        Task<TEntity> AddAsyn(TEntity t);
        int Count();
        Task<int> CountAsync();
        void Delete(TEntity entity);
        Task<int> DeleteAsyn(TEntity entity);
        void Dispose();
        TEntity? Find(Expression<Func<TEntity, bool>> match);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate);
        TEntity? FindById(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity?>> GetAllAsyn();
        Task<TEntity?> FindByIdAsync(TKey id);
        void Save();
        Task<int> SaveAsync();
        TEntity? Update(TEntity t, object key);
        Task<TEntity?> UpdateAsyn(TEntity t, TKey key);
    }

}
