using QuickFire.Application.Base;
using QuickFire.Core;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.Base
{
    public abstract class BaseService<T, TEntity> : IBaseInterface<TEntity> where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork<T> _unitOfWork;
        public BaseService(IUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TEntity Create(TEntity entity)
        {
            _unitOfWork.GetRepository<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            _unitOfWork.GetRepository<TEntity>().Delete(entity);
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.GetRepository<TEntity>().GetAll();
        }

        public TEntity? GetById(long id)
        {
            return _unitOfWork.GetRepository<TEntity>().FindById(id);
        }

        public TEntity Update(TEntity entity)
        {
            _unitOfWork.GetRepository<TEntity>().Update(entity);
            return entity;
        }
    }
}
