using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq.Expressions;

namespace QuickFire.Domain.Shared
{
    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : class, IEntity<long>
    { }
    public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, long> where TEntity : class, IEntity<long>
    { }
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : class
    {
        int Count();
        Task<int> CountAsync();
        void Dispose();
        TEntity? Find(Expression<Func<TEntity, bool>> match);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match);
        IEnumerable<TEntity?> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity? FindById(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> FindByIdAsync(TKey id);
    }
    public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        TEntity Add(TEntity t);
        Task<TEntity> AddAsync(TEntity t);
        int Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// 内部实现逻辑还是先根据ID查询再删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(TKey id);
        /// <summary>
        /// 内部实现逻辑还是先根据ID查询再删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsyn(TKey id);
        int Save();
        Task<int> SaveAsync();
        TEntity? Update(TEntity t);
        Task<TEntity?> UpdateAsync(TEntity t);

        /// <summary>
        /// 批量更新，不会更新基础字段需要人工指定
        /// </summary>
        /// <param name="setPropertyCalls"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 批量更新，不会更新基础字段需要人工指定
        /// </summary>
        /// <param name="setPropertyCalls"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int ExecuteUpdate(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 批量删除，该方法不会进入缓存
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 批量删除，该方法不会进入缓存
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int ExecuteDelete(Expression<Func<TEntity, bool>> predicate);

    }

}
