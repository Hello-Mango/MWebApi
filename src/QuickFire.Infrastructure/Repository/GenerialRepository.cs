using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Repository
{
    public class GenerialRepository<TEntity, TKey> : GenerialReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public GenerialRepository(DbContext dbContext) : base(dbContext)
        {
        }

        //protected readonly DbContext _context;
        //protected readonly DbSet<TEntity> _dbSet;

        //public GenerialRepository(DbContext dbContext)
        //{
        //    _context = dbContext;
        //    _dbSet = _context.Set<TEntity>();
        //}

        public virtual TEntity Add(TEntity t)
        {

            _dbSet.Add(t);
            _context.SaveChanges();
            return t;
        }

        public virtual async Task<TEntity> AddAsyn(TEntity t)
        {
            _dbSet.Add(t);
            await _context.SaveChangesAsync();
            return t;

        }



        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();

        }

        public virtual async Task<int> DeleteAsyn(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual TEntity? Update(TEntity t)
        {
            if (t == null)
                return null;
            TEntity? exist = _dbSet.Find(t.Id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<TEntity?> UpdateAsyn(TEntity t)
        {
            if (t == null)
                return null;
            TEntity? exist = await _dbSet.FindAsync(t.Id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
            }
            return exist;
        }


        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }






        public Task<int> ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ExecuteUpdateAsync(setPropertyCalls);
        }
        public int ExecuteUpdate(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ExecuteUpdate(setPropertyCalls);
        }

        public Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ExecuteDeleteAsync();
        }
        public int ExecuteDelete(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ExecuteDelete();
        }

    }

    public class GenerialReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenerialReadOnlyRepository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> query = _dbSet.Where(predicate);
            return query;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual async Task<IEnumerable<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public int Count()
        {
            return _dbSet.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsyn()
        {
            return await _dbSet.ToListAsync();
        }

        public TEntity? FindById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity?> FindByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual TEntity? Find(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _dbSet.SingleOrDefaultAsync(match);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.Where(match).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _dbSet.Where(match).ToListAsync();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
    }
}
