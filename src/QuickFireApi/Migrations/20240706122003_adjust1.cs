using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace QuickFireApi.Migrations
{
    /// <inheritdoc />
    public partial class adjust1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_user_role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false, comment: "创建时间"),
                    creator_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "创建人ID"),
                    creator_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "创建人编号"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "删除标记 0：否 1：是"),
                    deleted_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "删除员工编号"),
                    modified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "修改时间"),
                    modifier_staff_id = table.Column<long>(type: "bigint", nullable: false, comment: "修改员工ID"),
                    modifier_staff_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改员工编号"),
                    role_id = table.Column<long>(type: "bigint", nullable: false, comment: "角色Id"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "用户Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user_role", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }
    }
}
