using QuickFire.Application.Interface;
using QuickFire.Core;
using QuickFire.Domain.Entity;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.Services
{
    public class BaseService<T> : IBaseInterface<T> where T : BaseEntity
    {
        protected readonly IUnitOfWork<T> _unitOfWork;
        public BaseService(IUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public T Create(T entity)
        {
            _unitOfWork.GetRepository<T>().Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _unitOfWork.GetRepository<T>().Delete(entity);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _unitOfWork.GetRepository<T>().GetAll();
        }

        public T? GetById(long id)
        {
            return _unitOfWork.GetRepository<T>().FindById(id);
        }

        public T Update(T entity)
        {
            _unitOfWork.GetRepository<T>().Update(entity);
            return entity;
        }
    }
}
