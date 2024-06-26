using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Tls;
using QuickFire.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickFire.Extensions.EFData;
using QuickFire.Utils;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;


namespace QuickFire.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly UserContext _userContext;
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(UserContext userContext, DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        {
            _userContext = userContext;
            _options = options;
            _configuration = configuration;
        }

        public override int SaveChanges()
        {
            this.HandleSoftDelete(_userContext);
            this.HandleAddModify(_userContext);
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.HandleSoftDelete(_userContext);
            this.HandleAddModify(_userContext);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenant = _userContext.TenantId;
            optionsBuilder.UseSnakeCaseNamingConvention();
            IConfigurationSection sec = _configuration.GetSection("DataBase");
            string type = sec["DbType"];
            string connectionString = sec["ConnectionString"];
            switch (type)
            {
                case "sqlserver":
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case "mysql":
                    optionsBuilder.UseMySQL(connectionString);
                    break;
                case "pgsql":
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                default:
                    throw new Exception("Invalid database type");
            }
            //var connectionStr = _configuration.GetConnectionString(tenant);
            //optionsBuilder.UseSqlite(connectionStr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterAllEntities();
            modelBuilder.AddSoftDeleteQueryFilter();
            modelBuilder.AddTenantQueryFilter(_userContext);
        }
        private void HandleSoftDelete(UserContext userContext)
        {
            foreach (var entry in this.ChangeTracker.Entries<ISoftDeleted>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted = true;
                    if (entry.Entity is BaseEntity auditableEntity)
                    {
                        auditableEntity.ModifierStaffId = userContext.UserId;
                        auditableEntity.ModifierStaffName = userContext.UserName;
                        auditableEntity.ModificationTime = TimeUtils.GetTimeStamp();
                        auditableEntity.ModifiedAt = DateTimeOffset.UtcNow;
                    }
                }
            }
        }

        private void HandleAddModify(UserContext userContext)
        {
            foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModificationTime = TimeUtils.GetTimeStamp();
                    entry.Entity.ModifierStaffId = userContext.UserId;
                    entry.Entity.ModifierStaffName = userContext.UserName;
                    entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreationTime = TimeUtils.GetTimeStamp();
                    entry.Entity.CreatorStaffId = userContext.UserId;
                    entry.Entity.CreatorStaffName = userContext.UserName;
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow; ;
                }
            }
        }
    }

  

}
