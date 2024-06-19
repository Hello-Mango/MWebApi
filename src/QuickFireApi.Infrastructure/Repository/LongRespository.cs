using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.Repository
{
    public class LongIdRepository<T> : GenerialRepository<T, long> where T : class
    {
        public LongIdRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
