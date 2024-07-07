using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace QuickFireApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_app",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "应用编码"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "应用名称"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, comment: "应用描述"),
                    logo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "应用图标"),
                    visit_url = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "访问地址"),
                    is_operation = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否运行"),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: true, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_app", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_config",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    config_key = table.Column<string>(type: "longtext", nullable: false),
                    config_value = table.Column<string>(type: "longtext", nullable: false),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: true, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_config", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_tenant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "租户编号"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "租户名称"),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "租户描述"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户管理员"),
                    schema_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "租户类型"),
                    connection_string_id = table.Column<long>(type: "bigint", nullable: false, comment: "数据库连接字符串Id"),
                    db_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "数据库类型"),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: true, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_tenant", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "用户编号"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "用户名称"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "用户邮箱"),
                    password = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false, comment: "用户密码"),
                    mobile = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "用户手机号"),
                    is_lock = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "用户是否锁定标记 0：正常 1：锁定"),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: true, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_tenant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "longtext", nullable: false),
                    tenant_id = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user_tenant", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_sys_config",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    config_key = table.Column<string>(type: "longtext", nullable: false),
                    config_value = table.Column<string>(type: "longtext", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: true, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_sys_config", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_sys_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    no = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "角色编号"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "角色名称"),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户编号"),
                    decs = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "角色描述"),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: true, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_sys_role", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_sys_user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "用户Id"),
                    role_id = table.Column<long>(type: "bigint", nullable: false, comment: "角色Id"),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_sys_user_role", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_app");

            migrationBuilder.DropTable(
                name: "sys_config");

            migrationBuilder.DropTable(
                name: "sys_tenant");

            migrationBuilder.DropTable(
                name: "sys_user");

            migrationBuilder.DropTable(
                name: "sys_user_tenant");

            migrationBuilder.DropTable(
                name: "t_sys_config");

            migrationBuilder.DropTable(
                name: "t_sys_role");

            migrationBuilder.DropTable(
                name: "t_sys_user_role");
        }
    }
}
