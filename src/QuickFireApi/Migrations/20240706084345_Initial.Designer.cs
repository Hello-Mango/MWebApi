﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickFire.Infrastructure;

#nullable disable

namespace QuickFireApi.Migrations
{
    [DbContext(typeof(SysDbContext))]
    [Migration("20240706084345_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("longtext")
                        .HasColumnName("code");

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

                    b.Property<bool>("IsOperation")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_operation");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("logo");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at")
                        .HasComment("修改时间");

                    b.Property<long>("ModifierStaffId")
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
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("VisitUrl")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("visit_url");

                    b.HasKey("Id")
                        .HasName("pk_sys_app");

                    b.ToTable("sys_app", (string)null);
                });

            modelBuilder.Entity("QuickFire.Domain.Entites.SysRole", b =>
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
                        .HasColumnType("longtext")
                        .HasColumnName("decs");

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

                    b.Property<long>("ModifierStaffId")
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
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("No")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("no");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("tenant_id");

                    b.HasKey("Id")
                        .HasName("pk_sys_role");

                    b.ToTable("sys_role", (string)null);
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

                    b.Property<long>("ModifierStaffId")
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
#pragma warning restore 612, 618
        }
    }
}