using QuickFire.Application.Base;
using QuickFire.Core;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.Base
{
    public abstract class BaseService<T, TEntity> : IBaseInterface<TEntity> where TEntity : class, IEntity<long>
    {
        protected readonly IUnitOfWork<T> _unitOfWork;
        public BaseService(IUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TEntity Create(TEntity entity)
        {
            _unitOfWork.GetLongRepository<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _unitOfWork.GetLongRepository<TEntity>().AddAsync(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            _unitOfWork.GetLongRepository<TEntity>().Delete(entity);
            return entity;
        }

        public int Delete(long id)
        {
            return _unitOfWork.GetLongRepository<TEntity>().Delete(id);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            await _unitOfWork.GetLongRepository<TEntity>().DeleteAsync(entity);
            return entity;
        }

        public async Task<int> DeleteAsync(long id)
        {
            return await _unitOfWork.GetLongRepository<TEntity>().DeleteAsyn(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.GetLongRepository<TEntity>().GetAll();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _unitOfWork.GetLongRepository<TEntity>().GetAllAsync();
        }

        public IEnumerable<TEntity?> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return _unitOfWork.GetLongRepository<TEntity>().FindBy(filter);
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _unitOfWork.GetLongRepository<TEntity>().FindByAsync(filter);
        }

        public TEntity? GetById(long id)
        {
            return _unitOfWork.GetLongRepository<TEntity>().FindById(id);
        }

        public async Task<TEntity?> GetByIdAsync(long id)
        {
            return await _unitOfWork.GetLongRepository<TEntity>().FindByIdAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            _unitOfWork.GetLongRepository<TEntity>().Update(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _unitOfWork.GetLongRepository<TEntity>().UpdateAsync(entity);
            return entity;
        }
    }
}
