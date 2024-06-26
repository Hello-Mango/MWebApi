using QuickFire.Core;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.Interface
{
    public interface IBaseInterface<T> where T : BaseEntity
    {
        public T Create(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        public T? GetById(long id);
        public IEnumerable<T> GetAll();
    }
}
