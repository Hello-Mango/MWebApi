using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using QuickFire.Domain.Entity.Base;
using QuickFire.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Repository
{
    public class LongIdRepository<T> : GenerialRepository<T, long>, IRepository<T> where T : BaseEntity
    {
        public LongIdRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
    public class LongIdReadOnlyRepository<T> : GenerialReadOnlyRepository<T, long>, IReadOnlyRepository<T> where T : BaseEntity
    {
        public LongIdReadOnlyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
