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
                    code = table.Column<string>(type: "longtext", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    logo = table.Column<string>(type: "longtext", nullable: false),
                    visit_url = table.Column<string>(type: "longtext", nullable: false),
                    is_operation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "修改员工ID"),
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
                name: "sys_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    no = table.Column<string>(type: "longtext", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    tenant_id = table.Column<string>(type: "longtext", nullable: false),
                    decs = table.Column<string>(type: "longtext", nullable: false),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_role", x => x.id);
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
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "修改员工ID"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_app");

            migrationBuilder.DropTable(
                name: "sys_role");

            migrationBuilder.DropTable(
                name: "sys_user");
        }
    }
}
