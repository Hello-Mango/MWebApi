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
    public interface IBaseInterface<T> where T : class, IEntity<long>
    {
        public T Create(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        public int Delete(long id);
        public T? GetById(long id);
        public IEnumerable<T> GetAll();
        public IEnumerable<T?> GetByFilter(Expression<Func<T, bool>> filter);



        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<T> DeleteAsync(T entity);
        public Task<int> DeleteAsync(long id);
        public Task<T?> GetByIdAsync(long id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
    }
}
