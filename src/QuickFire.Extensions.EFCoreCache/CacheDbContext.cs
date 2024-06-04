using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Extensions.EFCoreCache
{
    public class CacheDbContext : DbContext
    {
        public DbSet<CacheItem> CacheItems { get; set; }

        public CacheDbContext(DbContextOptions<CacheDbContext> options)
            : base(options)
        {
        }
    }
    public class CacheItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}
