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
using QuickFire.Extensions.Core;
using QuickFire.Infrastructure.Extensions;
using QuickFire.Extensions.AuditLog;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using ShardingCore.Sharding.Abstractions;
using ShardingCore.Sharding;
using ShardingCore.Core.VirtualRoutes.TableRoutes.RouteTails.Abstractions;
using Microsoft.Extensions.Options;
using QuickFire.Domain.Shared;


namespace QuickFire.Infrastructure
{
    public class SysDbContext : AbstractShardingDbContext, IShardingTableDbContext
    {
        private readonly DbContextOptions<SysDbContext> _options;
        private readonly ISessionContext _sessionContext;
        private readonly IConfiguration _configuration;
        private readonly String _dbType;
        private readonly String _connectionString;

        public IRouteTail RouteTail { get; set; }

        public SysDbContext(ISessionContext sessionContext, DbContextOptions<SysDbContext> options, IConfiguration configuration, IOptions<AppSettings> databaseOption) : base(options)
        {
            _sessionContext = sessionContext;
            _options = options;
            _configuration = configuration;
            _dbType = databaseOption.Value.DataBaseConfig.DbType.ToLower();
            _connectionString = databaseOption.Value.DataBaseConfig.ConnectionString;

        }

        public override int SaveChanges()
        {
            this.HandleSoftDelete(_sessionContext);
            this.HandleAddModify(_sessionContext);
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.HandleSoftDelete(_sessionContext);
            this.HandleAddModify(_sessionContext);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();

            switch (_dbType)
            {
                case "sqlserver":
                    optionsBuilder.UseSqlServer(_connectionString, b => b.MigrationsAssembly("QuickFireApi"));
                    break;
                case "mysql":
                    optionsBuilder.UseMySQL(_connectionString, b => b.MigrationsAssembly("QuickFireApi"));
                    break;
                case "pgsql":
                    optionsBuilder.UseNpgsql(_connectionString, b => b.MigrationsAssembly("QuickFireApi"));
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
            modelBuilder.AddTenantQueryFilter(_sessionContext);
            if (_dbType != "pgsql" && _dbType != "sqlserver")
            {
                modelBuilder.AddDateTimeOffsetConvert();
                modelBuilder.AddDateTimeConvert();
            }
        }



        private void HandleSoftDelete(ISessionContext sessionContext)
        {
            foreach (var entry in this.ChangeTracker.Entries<ISoftDeleted>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted = true;
                    if (entry.Entity is BaseEntity auditableEntity)
                    {
                        auditableEntity.ModifierStaffId = sessionContext.UserId;
                        auditableEntity.ModifierStaffNo = sessionContext.UserName;
                        auditableEntity.ModifiedAt = DateTimeOffset.UtcNow;
                    }
                }
            }
        }

        private void HandleAddModify(ISessionContext sessionContext)
        {
            foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifierStaffId = sessionContext.UserId;
                    entry.Entity.ModifierStaffNo = sessionContext.UserName;
                    entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatorStaffId = sessionContext.UserId;
                    entry.Entity.CreatorStaffNo = sessionContext.UserName;
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow; ;
                }
            }
        }

        private void AddAuditLog(ISessionContext sessionContext, IAuditLogger auditLogger)
        {
            if (_configuration.GetSection("AuditLog").GetValue<bool>("DbEnable") == false)
            {
                return;
            }
            List<AuditLog> auditLogs = new List<AuditLog>();

            foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
            {
                AuditLog auditLog = new AuditLog()
                {
                    Action = entry.State.ToString(),
                    Entity = entry.Entity.GetType().Name,
                    CreatedTime = DateTime.UtcNow,
                    UserId = sessionContext.UserId.ToString(),
                    UserName = sessionContext.UserName
                };
                switch (entry.State)
                {
                    case EntityState.Added:
                        var propertyList = entry.CurrentValues.Properties.Where(i => entry.Property(i.Name).IsModified);
                        PropertyEntry keyEntity = entry.Property("KeyId");
                        foreach (var prop in propertyList)
                        {
                            PropertyEntry entity = entry.Property(prop.Name)!;
                            if (entity != null)
                            {
                                EntityChangeInfo entityChangeInfo = new EntityChangeInfo()
                                {
                                    OldValue = string.Empty,
                                    NewValue = entity.CurrentValue == null ? string.Empty : entity.CurrentValue.ToString()!,
                                };
                                auditLog!.EntityChanges!.Add(entityChangeInfo);
                            }
                        }
                        break;
                    case EntityState.Modified:
                        propertyList = entry.CurrentValues.Properties.Where(i => entry.Property(i.Name).IsModified);
                        keyEntity = entry.Property("KeyId");
                        foreach (var prop in propertyList)
                        {
                            PropertyEntry entity = entry.Property(prop.Name)!;
                            if (entity != null)
                            {
                                EntityChangeInfo entityChangeInfo = new EntityChangeInfo()
                                {
                                    OldValue = entity.OriginalValue == null ? string.Empty : entity.OriginalValue.ToString()!,
                                    NewValue = entity.CurrentValue == null ? string.Empty : entity.CurrentValue.ToString()!,
                                };
                                auditLog!.EntityChanges!.Add(entityChangeInfo);
                            }
                        }

                        break;
                    case EntityState.Deleted:
                        propertyList = entry.CurrentValues.Properties.Where(i => entry.Property(i.Name).IsModified);
                        keyEntity = entry.Property("KeyId");
                        foreach (var prop in propertyList)
                        {
                            PropertyEntry entity = entry.Property(prop.Name)!;
                            if (entity != null)
                            {
                                EntityChangeInfo entityChangeInfo = new EntityChangeInfo()
                                {
                                    OldValue = entity.OriginalValue == null ? string.Empty : entity.OriginalValue.ToString()!,
                                };
                                auditLog!.EntityChanges!.Add(entityChangeInfo);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }



}
