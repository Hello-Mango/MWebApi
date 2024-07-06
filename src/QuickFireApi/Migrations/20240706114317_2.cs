using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickFireApi.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "tenant_id",
                table: "sys_role",
                type: "bigint",
                nullable: false,
                comment: "租户编号",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "no",
                table: "sys_role",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                comment: "角色编号",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "角色名称",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "decs",
                table: "sys_role",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "角色描述",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "visit_url",
                table: "sys_app",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "访问地址",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_app",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "应用名称",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "logo",
                table: "sys_app",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "应用图标",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<bool>(
                name: "is_operation",
                table: "sys_app",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否运行",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "sys_app",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "应用编码",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "sys_app",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                comment: "应用描述");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "sys_app");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "sys_role",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "租户编号");

            migrationBuilder.AlterColumn<string>(
                name: "no",
                table: "sys_role",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32,
                oldComment: "角色编号");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_role",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "角色名称");

            migrationBuilder.AlterColumn<string>(
                name: "decs",
                table: "sys_role",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldComment: "角色描述");

            migrationBuilder.AlterColumn<string>(
                name: "visit_url",
                table: "sys_app",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldComment: "访问地址");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_app",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "应用名称");

            migrationBuilder.AlterColumn<string>(
                name: "logo",
                table: "sys_app",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldComment: "应用图标");

            migrationBuilder.AlterColumn<bool>(
                name: "is_operation",
                table: "sys_app",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否运行");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "sys_app",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "应用编码");
        }
    }
}
