﻿using Microsoft.EntityFrameworkCore;
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
using QuickFire.Domain.Entity;
using QuickFire.Extensions.Core;
using QuickFire.Infrastructure.Extensions;
using QuickFire.Extensions.AuditLog;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;


namespace QuickFire.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly IUserContext _userContext;
        private readonly IConfiguration _configuration;
        private readonly String _dbType;
        private readonly String _connectionString;
        public ApplicationDbContext(IUserContext userContext, DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        {
            _userContext = userContext;
            _options = options;
            _configuration = configuration;
            IConfigurationSection sec = _configuration.GetSection("DataBase");
            _dbType = sec["DbType"]!;
            _connectionString = sec["ConnectionString"]!;

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

            switch (_dbType)
            {
                case "sqlserver":
                    optionsBuilder.UseSqlServer(_connectionString);
                    break;
                case "mysql":
                    optionsBuilder.UseMySQL(_connectionString);
                    break;
                case "pgsql":
                    optionsBuilder.UseNpgsql(_connectionString);
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
            if (_dbType != "pgsql" && _dbType != "sqlserver")
            {
                modelBuilder.AddDateTimeOffsetConvert();
                modelBuilder.AddDateTimeConvert();
            }
        }



        private void HandleSoftDelete(IUserContext userContext)
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

        private void HandleAddModify(IUserContext userContext)
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

        private void AddAuditLog(IUserContext userContext, IAuditLogger auditLogger)
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
                    UserId = userContext.UserId.ToString(),
                    UserName = userContext.UserName
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
