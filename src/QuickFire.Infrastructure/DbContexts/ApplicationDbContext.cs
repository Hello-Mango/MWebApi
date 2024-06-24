using Microsoft.EntityFrameworkCore;
using QuickFire.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly UserContext _userContext;
        public ApplicationDbContext(UserContext userContext,  DbContextOptions<ApplicationDbContext> options) 
        {
            _userContext = userContext;
            _options = options;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenant = _userContext.TenantId;
            //var connectionStr = _configuration.GetConnectionString(tenant);
            //optionsBuilder.UseSqlite(connectionStr);
        }
    }
}
