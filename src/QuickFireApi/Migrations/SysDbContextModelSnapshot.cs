﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickFire.Infrastructure;

#nullable disable

namespace QuickFireApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class SysDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("QQuickFire.Domain.Entites.SysApp", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("code")
                        .HasComment("应用编码");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasComment("创建时间");

                    b.Property<long>("CreatorStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_staff_id")
                        .HasComment("创建人ID");

                    b.Property<string>("CreatorStaffNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creator_staff_no")
                        .HasComment("创建人编号");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("删除标记 0：否 1：是");

                    b.Property<string>("DeletedStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("deleted_staff_no")
                        .HasComment("删除员工编号");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description")
                        .HasComment("应用描述");

                    b.Property<bool>("IsOperation")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_operation")
                        .HasComment("是否运营系统");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("logo")
                        .HasComment("应用图标");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at")
                        .HasComment("修改时间");

                    b.Property<long?>("ModifierStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("modifier_staff_id")
                        .HasComment("修改员工ID");

                    b.Property<string>("ModifierStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modifier_staff_no")
                        .HasComment("修改员工编号");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("应用名称");

                    b.Property<string>("VisitUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("visit_url")
                        .HasComment("访问地址");

                    b.HasKey("Id")
                        .HasName("pk_sys_app");

                    b.ToTable("sys_app", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.SysConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("ConfigKey")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("config_key")
                        .HasComment("配置键");

                    b.Property<string>("ConfigValue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("config_value")
                        .HasComment("配置值");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasComment("创建时间");

                    b.Property<long>("CreatorStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_staff_id")
                        .HasComment("创建人ID");

                    b.Property<string>("CreatorStaffNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creator_staff_no")
                        .HasComment("创建人编号");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("删除标记 0：否 1：是");

                    b.Property<string>("DeletedStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("deleted_staff_no")
                        .HasComment("删除员工编号");

                    b.Property<string>("Field1")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("field1")
                        .HasComment("备用字段1");

                    b.Property<string>("Field2")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("field2")
                        .HasComment("备用字段2");

                    b.Property<string>("Field3")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("field3")
                        .HasComment("备用字段3");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at")
                        .HasComment("修改时间");

                    b.Property<long?>("ModifierStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("modifier_staff_id")
                        .HasComment("修改员工ID");

                    b.Property<string>("ModifierStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modifier_staff_no")
                        .HasComment("修改员工编号");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("remark")
                        .HasComment("备注");

                    b.HasKey("Id")
                        .HasName("pk_sys_config");

                    b.ToTable("sys_config", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.SysTenant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("ConnectionStringId")
                        .HasColumnType("bigint")
                        .HasColumnName("connection_string_id")
                        .HasComment("数据库连接字符串Id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasComment("创建时间");

                    b.Property<long>("CreatorStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_staff_id")
                        .HasComment("创建人ID");

                    b.Property<string>("CreatorStaffNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creator_staff_no")
                        .HasComment("创建人编号");

                    b.Property<string>("DbType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("db_type")
                        .HasComment("数据库类型");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("删除标记 0：否 1：是");

                    b.Property<string>("DeletedStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("deleted_staff_no")
                        .HasComment("删除员工编号");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description")
                        .HasComment("租户描述");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at")
                        .HasComment("修改时间");

                    b.Property<long?>("ModifierStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("modifier_staff_id")
                        .HasComment("修改员工ID");

                    b.Property<string>("ModifierStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modifier_staff_no")
                        .HasComment("修改员工编号");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("租户名称");

                    b.Property<string>("No")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("no")
                        .HasComment("租户编号");

                    b.Property<string>("SchemaType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("schema_type")
                        .HasComment("租户类型");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id")
                        .HasComment("租户管理员");

                    b.HasKey("Id")
                        .HasName("pk_sys_tenant");

                    b.ToTable("sys_tenant", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.SysUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasComment("创建时间");

                    b.Property<long>("CreatorStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_staff_id")
                        .HasComment("创建人ID");

                    b.Property<string>("CreatorStaffNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creator_staff_no")
                        .HasComment("创建人编号");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("删除标记 0：否 1：是");

                    b.Property<string>("DeletedStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("deleted_staff_no")
                        .HasComment("删除员工编号");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email")
                        .HasComment("用户邮箱");

                    b.Property<bool>("IsLock")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_lock")
                        .HasComment("用户是否锁定标记 0：正常 1：锁定");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("mobile")
                        .HasComment("用户手机号");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at")
                        .HasComment("修改时间");

                    b.Property<long?>("ModifierStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("modifier_staff_id")
                        .HasComment("修改员工ID");

                    b.Property<string>("ModifierStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modifier_staff_no")
                        .HasComment("修改员工编号");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("用户名称");

                    b.Property<string>("No")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("no")
                        .HasComment("用户编号");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("password")
                        .HasComment("用户密码");

                    b.HasKey("Id")
                        .HasName("pk_sys_user");

                    b.ToTable("sys_user", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.SysUserTenant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id")
                        .HasComment("租户Id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id")
                        .HasComment("用户Id");

                    b.HasKey("Id")
                        .HasName("pk_sys_user_tenant");

                    b.ToTable("sys_user_tenant", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.TSysConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("ConfigKey")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("config_key")
                        .HasComment("配置键");

                    b.Property<string>("ConfigValue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("config_value")
                        .HasComment("配置值");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasComment("创建时间");

                    b.Property<long>("CreatorStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_staff_id")
                        .HasComment("创建人ID");

                    b.Property<string>("CreatorStaffNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creator_staff_no")
                        .HasComment("创建人编号");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("删除标记 0：否 1：是");

                    b.Property<string>("DeletedStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("deleted_staff_no")
                        .HasComment("删除员工编号");

                    b.Property<string>("Field1")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("field1")
                        .HasComment("备用字段1");

                    b.Property<string>("Field2")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("field2")
                        .HasComment("备用字段2");

                    b.Property<string>("Field3")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("field3")
                        .HasComment("备用字段3");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at")
                        .HasComment("修改时间");

                    b.Property<long?>("ModifierStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("modifier_staff_id")
                        .HasComment("修改员工ID");

                    b.Property<string>("ModifierStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modifier_staff_no")
                        .HasComment("修改员工编号");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("remark")
                        .HasComment("备注");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id")
                        .HasComment("租户ID");

                    b.HasKey("Id")
                        .HasName("pk_t_sys_config");

                    b.ToTable("t_sys_config", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.TSysRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasComment("创建时间");

                    b.Property<long>("CreatorStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_staff_id")
                        .HasComment("创建人ID");

                    b.Property<string>("CreatorStaffNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creator_staff_no")
                        .HasComment("创建人编号");

                    b.Property<string>("Decs")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("decs")
                        .HasComment("角色描述");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("删除标记 0：否 1：是");

                    b.Property<string>("DeletedStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("deleted_staff_no")
                        .HasComment("删除员工编号");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at")
                        .HasComment("修改时间");

                    b.Property<long?>("ModifierStaffId")
                        .HasColumnType("bigint")
                        .HasColumnName("modifier_staff_id")
                        .HasComment("修改员工ID");

                    b.Property<string>("ModifierStaffNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modifier_staff_no")
                        .HasComment("修改员工编号");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("角色名称");

                    b.Property<string>("No")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("no")
                        .HasComment("角色编号");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id")
                        .HasComment("租户编号");

                    b.HasKey("Id")
                        .HasName("pk_t_sys_role");

                    b.ToTable("t_sys_role", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.TSysUserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id")
                        .HasComment("角色Id");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id")
                        .HasComment("租户Id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id")
                        .HasComment("用户Id");

                    b.HasKey("Id")
                        .HasName("pk_t_sys_user_role");

                    b.ToTable("t_sys_user_role", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
